import { createIsomorphicFn } from '@tanstack/react-start'
import { getRequestHeaders } from '@tanstack/react-start/server'

export class ApiError extends Error {
  constructor(
    public status: number,
    message: string,
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

export async function apiFetch<T>(
  path: string,
  requestInit?: Omit<RequestInit, 'body'> & { body?: unknown },
): Promise<T> {
  console.log('BASE URL:', getBaseUrl())

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
  const contentType = res.headers.get('content-type')
  const data = contentType?.includes('json')
    ? await res.json()
    : await res.text()

  if (!res.ok) {
    const message =
      typeof data === 'object' && data !== null
        ? (data.title ?? data.message ?? `HTTP ${res.status}`)
        : data || `HTTP ${res.status}`
    throw new ApiError(res.status, message)
  }

  return data as T
}
