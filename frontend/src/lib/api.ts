const BASE_URL = import.meta.env.VITE_API as string

type ProblemDetails = {
  title?: string
  status?: number
  detail?: string
  errors?: Record<string, string[]>
}

export class ApiError extends Error {
  constructor(
    public status: number,
    message: string,
    public detail?: string,
    public errors?: Record<string, string[]>,
  ) {
    super(message)
  }
}

function parseError(status: number, data: unknown): ApiError {
  if (typeof data === 'object' && data !== null) {
    const p = data as ProblemDetails
    return new ApiError(status, p.title ?? `HTTP ${status}`, p.detail, p.errors)
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

async function refreshToken(): Promise<boolean> {
  try {
    const res = await fetch(`${BASE_URL}/auth/refresh-token`, {
      method: 'POST',
      credentials: 'include',
    })
    return res.ok
  } catch {
    return false
  }
}

export async function apiFetch<T>(
  path: string,
  init?: ApiRequestInit,
): Promise<T> {
  let res = await request(path, init)

  if (res.status === 401) {
    const ok = await refreshToken()
    if (ok) {
      res = await request(path, init)
    }
  }

  return parseResponse<T>(res)
}
