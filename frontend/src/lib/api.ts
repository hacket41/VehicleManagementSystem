import { redirect } from '@tanstack/react-router'
import { createIsomorphicFn } from '@tanstack/react-start'

const URL = import.meta.env.VITE_API

type Problem = {
  status?: number
  detail?: string
  title?: string
}

let isRefreshing = false
let refreshPromise: Promise<boolean> | null = null

const getForwardHeaders = createIsomorphicFn()
  .client(() => new Headers())
  .server(async () => {
    const { getRequestHeaders } = await import('@tanstack/react-start/server')
    const cookie = getRequestHeaders().get('cookie')
    const headers = new Headers()
    if (cookie) {
      headers.set('cookie', cookie)
    }
    return headers
  })

async function tryRefreshTokens(): Promise<boolean> {
  if (isRefreshing && refreshPromise) {
    return refreshPromise
  }

  isRefreshing = true
  const headers = await getForwardHeaders()
  refreshPromise = fetch(`${URL}/auth/refresh-token`, {
    method: 'POST',
    credentials: 'include',
    headers: Object.fromEntries(headers.entries()),
  })
    .then((res) => res.ok)
    .catch(() => false)
    .finally(() => {
      isRefreshing = false
      refreshPromise = null
    })
  return refreshPromise
}

async function executeRequest(
  path: string,
  init?: RequestInit,
): Promise<Response> {
  const forwardHeaders = await getForwardHeaders()
  const mergedHeaders = new Headers(forwardHeaders)

  let body = init?.body

  if (body && typeof body === 'object' && !(body instanceof FormData)) {
    body = JSON.stringify(body)
    mergedHeaders.set('Content-Type', 'application/json')
  }

  if (init?.headers) {
    const initHeaders = new Headers(init.headers)
    initHeaders.forEach((value, key) => {
      mergedHeaders.set(key, value)
    })
  }

  console.log('[SSR Fetched]:', path)
  return fetch(`${URL}${path}`, {
    ...init,
    credentials: 'include',
    headers: mergedHeaders,
  })
}

export async function apiFetch<T, B = unknown>(
  path: string,
  init?: RequestInit & { body?: B },
): Promise<T> {
  try {
    let res = await executeRequest(path, init)
    if (res.status === 401) {
      const refreshed = await tryRefreshTokens()

      if (!refreshed) {
        // window.location.href = '/login'
        throw redirect({
          to: '/login',
        })
      }
      res = await executeRequest(path, init)

      if (res.status === 401) {
        // window.location.href = '/login'
        throw redirect({
          to: '/login',
        })
      }
    }

    const contentType = res.headers.get('content-type')
    let data: unknown = null
    if (contentType?.includes('application/json')) {
      data = await res.json()
    } else {
      data = await res.text()
    }

    if (!res.ok) {
      if (typeof data === 'object' && data !== null) {
        const problem = data as Problem

        throw new Error(problem.detail || problem.title || `API ${res.status}`)
      }

      throw new Error(
        typeof data === 'string'
          ? data
          : `API ${res.status}: ${res.statusText}`,
      )
    }

    return data as T
  } catch (err) {
    if (err instanceof TypeError) {
      throw new Error('Network error: Unable to reach server')
    }
    throw err
  }
}
