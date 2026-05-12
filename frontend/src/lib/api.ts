import { createIsomorphicFn } from '@tanstack/react-start'
import { getRequestHeaders } from '@tanstack/react-start/server'

export class ApiError extends Error {
  constructor(
    public status: number,
    message: string,
    public errors?: string[],
  ) {
    super(message)
    this.name = 'ApiError'
  }
}

const getBaseUrl = () =>
  typeof window === 'undefined'
    ? (process.env.API_URL as string) // server
    : (import.meta.env.VITE_API as string) // client

const getForwardHeaders = createIsomorphicFn()
  .client(() => new Headers())
  .server(async () => {
    const cookie = getRequestHeaders().get('cookie')
    const headers = new Headers()
    if (cookie) {
      headers.set('cookie', cookie)
    }
    return headers
  })

let refreshPromise: Promise<void> | null = null

async function attemptRefresh(): Promise<void> {
  if (refreshPromise) {
    return refreshPromise
  }
  refreshPromise = doRefresh().finally(() => {
    refreshPromise = null
  })
  return refreshPromise
}

export async function doRefresh(): Promise<void> {
  const extraHeaders = await getForwardHeaders()
  const headers = new Headers(extraHeaders)

  // for (const [key, value] of extraHeaders.entries()) {
  //   headers.set(key, value)
  // }
  const res = await fetch(`${getBaseUrl()}/api/auth/refresh-token`, {
    method: 'POST',
    credentials: 'include',
    headers,
  })

  if (!res.ok) {
    throw new ApiError(res.status, 'Session expired. Please log in again.')
  }
}

export async function apiFetch<T>(
  path: string,
  requestInit?: Omit<RequestInit, 'body'> & { body?: unknown },
  _isRetry = false,
): Promise<T> {
  const extraHeaders = await getForwardHeaders()
  const headers = new Headers(requestInit?.headers)

  for (const [key, value] of extraHeaders.entries()) {
    headers.set(key, value)
  }

  let body: BodyInit | undefined
  if (requestInit?.body != null) {
    if (
      requestInit.body instanceof FormData ||
      requestInit.body instanceof URLSearchParams ||
      typeof requestInit.body === 'string'
    ) {
      body = requestInit.body
    } else {
      body = JSON.stringify(requestInit.body)
      headers.set('Content-Type', 'application/json')
    }
  }

  const res = await fetch(`${getBaseUrl()}${path}`, {
    ...requestInit,
    body,
    headers,
    credentials: 'include',
  })

  if (res.status === 401 && !_isRetry) {
    try {
      await attemptRefresh()
      return apiFetch<T>(path, requestInit, true)
    } catch {
      throw new ApiError(res.status, 'Session expired. Please log in again.')
    }
  }
  const contentType = res.headers.get('content-type')
  const data = contentType?.includes('json')
    ? await res.json()
    : await res.text()

  if (!res.ok) {
    const errors: string[] | undefined =
      Array.isArray(data?.errors) && data.errors.length > 0
        ? data.errors
        : undefined

    const message =
      errors?.[0] ?? data?.title ?? data?.message ?? `HTTP ${res.status}`

    throw new ApiError(res.status, message, errors)
  }

  return data as T
}
