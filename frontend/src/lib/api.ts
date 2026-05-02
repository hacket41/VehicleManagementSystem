import { redirect } from '@tanstack/react-router'
import { createIsomorphicFn } from '@tanstack/react-start'

const BASE_URL = import.meta.env.VITE_API as string

if (import.meta.env.DEV && !BASE_URL) {
  console.warn('[apiFetch] VITE_API is not set. All requests will fail.')
}

type ProblemDetails = {
  type?: string
  title?: string
  status?: number
  detail?: string
  instance?: string
  errors?: Record<string, string[]>
  traceId?: string
}

export class ApiError extends Error {
  constructor(
    public readonly status: number,
    message: string,
    public readonly detail?: string,
    public readonly validationErrors?: Record<string, string[]>,
    public readonly traceId?: string,
  ) {
    super(message)
    this.name = 'ApiError'
  }
}

const getForwardHeaders = createIsomorphicFn()
  .client(() => new Headers())
  .server(async () => {
    const { getRequestHeaders } = await import('@tanstack/react-start/server')
    const cookie = getRequestHeaders().get('cookie')
    const headers = new Headers()
    if (cookie) headers.set('cookie', cookie)
    return headers
  })

type QueueItem = {
  resolve: (refreshed: boolean) => void
  reject: (err: unknown) => void
}

let isRefreshing = false
let pendingQueue: QueueItem[] = []

function drainQueue(success: boolean, err?: unknown): void {
  for (const item of pendingQueue) {
    success ? item.resolve(true) : item.reject(err)
  }
  pendingQueue = []
}

async function tryRefreshTokens(): Promise<boolean> {
  if (isRefreshing) {
    return new Promise<boolean>((resolve, reject) => {
      pendingQueue.push({ resolve, reject })
    })
  }

  isRefreshing = true
  try {
    const res = await fetch(`${BASE_URL}/auth/refresh-token`, {
      method: 'POST',
      credentials: 'include',
    })
    drainQueue(res.ok)
    return res.ok
  } catch (err) {
    drainQueue(false, err)
    return false
  } finally {
    isRefreshing = false
  }
}

async function executeRequest(
  path: string,
  init?: Omit<RequestInit, 'body'> & { body?: unknown },
): Promise<Response> {
  const { body: rawBody, headers: callerHeaders, ...restInit } = init ?? {}

  const forwardHeaders = await getForwardHeaders()
  const mergedHeaders = new Headers(forwardHeaders)

  let serializedBody: BodyInit | undefined
  if (rawBody != null) {
    if (rawBody instanceof FormData || rawBody instanceof URLSearchParams) {
      serializedBody = rawBody
    } else if (typeof rawBody === 'string') {
      serializedBody = rawBody
    } else {
      serializedBody = JSON.stringify(rawBody)
      mergedHeaders.set('Content-Type', 'application/json')
    }
  }

  if (callerHeaders) {
    new Headers(callerHeaders).forEach((value, key) => {
      mergedHeaders.set(key, value)
    })
  }

  return fetch(`${BASE_URL}${path}`, {
    ...restInit,
    body: serializedBody,
    credentials: 'include',
    headers: mergedHeaders,
  })
}

function isJsonResponse(contentType: string | null): boolean {
  if (!contentType) return false
  return (
    contentType.includes('application/json') || contentType.includes('+json')
  )
}

// function toApiError(status: number, data: unknown): ApiError {
//   if (typeof data === 'object' && data !== null) {
//     const p = data as ProblemDetails
//     return new ApiError(
//       status,
//       p.title ?? `HTTP ${status}`,
//       p.detail,
//       p.errors,
//       p.traceId,
//     )
//   }
//   return new ApiError(
//     status,
//     typeof data === 'string' ? data : `HTTP ${status}`,
//   )
// }

function toApiError(status: number, data: unknown): ApiError {
  if (typeof data === 'object' && data !== null) {
    const p = data as ProblemDetails

    if (Array.isArray(p.errors)) {
      return new ApiError(status, p.errors[0] ?? `HTTP ${status}`) // ← "Invalid Credentials"
    }

    return new ApiError(
      status,
      p.title ?? `HTTP ${status}`,
      p.detail,
      p.errors,
      p.traceId,
    )
  }
  return new ApiError(
    status,
    typeof data === 'string' ? data : `HTTP ${status}`,
  )
}
const isServer = typeof window === 'undefined'

export async function apiFetch<T, B = unknown>(
  path: string,
  init?: Omit<RequestInit, 'body'> & { body?: B },
): Promise<T> {
  try {
    let res = await executeRequest(path, init)

    if (res.status === 401) {
      if (isServer) {
        throw redirect({ to: '/login' })
      }

      const refreshed = await tryRefreshTokens()
      if (!refreshed) throw redirect({ to: '/login' })

      res = await executeRequest(path, init)

      if (res.status === 401) throw redirect({ to: '/login' })
    }

    const data: unknown = isJsonResponse(res.headers.get('content-type'))
      ? await res.json()
      : await res.text()

    if (!res.ok) throw toApiError(res.status, data)

    return data as T
  } catch (err) {
    if (err instanceof TypeError) {
      throw new ApiError(0, 'Network error: Unable to reach the server')
    }
    throw err
  }
}
