const BASE_URL = import.meta.env.VITE_API as string
const REQUEST_TIMEOUT_MS = 15_000

type ProblemDetails = {
  title?: string
  status?: number
  detail?: string
  errors?: Record<string, string[]>
  message?: string
}

export class ApiError extends Error {
  constructor(
    public status: number,
    message: string,
    public detail?: string,
    public errors?: Record<string, string[]>,
  ) {
    super(message)
    this.name = 'ApiError'
  }
}

function parseError(status: number, data: unknown): ApiError {
  if (typeof data === 'object' && data !== null) {
    const p = data as ProblemDetails
    const firstError = p.errors ? Object.values(p.errors).flat()[0] : undefined
    const message = p.title ?? p.message ?? firstError ?? `HTTP ${status}`
    return new ApiError(status, message, p.detail, p.errors)
  }
  return new ApiError(
    status,
    typeof data === 'string' ? data : `HTTP ${status}`,
  )
}

type ApiRequestInit = Omit<RequestInit, 'body'> & {
  body?: unknown
}

async function request(path: string, init?: ApiRequestInit): Promise<Response> {
  const headers = new Headers(init?.headers)
  let body: BodyInit | undefined

  if (init?.body != null) {
    if (
      init.body instanceof FormData ||
      init.body instanceof URLSearchParams ||
      typeof init.body === 'string'
    ) {
      body = init.body
    } else {
      body = JSON.stringify(init.body)
      headers.set('Content-Type', 'application/json')
    }
  }

  return fetch(`${BASE_URL}${path}`, {
    ...init,
    body,
    headers,
    credentials: 'include',
    // Prevents requests from hanging forever
    signal: AbortSignal.timeout(REQUEST_TIMEOUT_MS),
  })
}

async function parseResponse<T>(res: Response): Promise<T> {
  const contentType = res.headers.get('content-type')
  const data = contentType?.includes('json')
    ? await res.json()
    : await res.text()
  if (!res.ok) throw parseError(res.status, data)
  return data as T
}

let refreshPromise: Promise<boolean> | null = null

async function refreshToken(): Promise<boolean> {
  if (refreshPromise) return refreshPromise

  refreshPromise = fetch(`${BASE_URL}/api/auth/refresh-token`, {
    method: 'POST',
    credentials: 'include',
    signal: AbortSignal.timeout(REQUEST_TIMEOUT_MS),
  })
    .then((res) => res.ok)
    .catch(() => false)
    .finally(() => {
      // Clear the lock whether it succeeded or failed
      refreshPromise = null
    })

  return refreshPromise
}

export async function apiFetch<T>(
  path: string,
  init?: ApiRequestInit,
): Promise<T> {
  const res = await request(path, init)

  if (res.status === 401) {
    const refreshed = await refreshToken()
    if (!refreshed) {
      // Refresh failed — throw immediately, don't retry
      throw new ApiError(401, 'Session expired. Please log in again.')
    }
    // Refresh succeeded — retry the original request once
    return parseResponse<T>(await request(path, init))
  }

  return parseResponse<T>(res)
}
