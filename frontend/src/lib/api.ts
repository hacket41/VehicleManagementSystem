const URL = import.meta.env.VITE_API

type Problem = {
  status?: number
  detail?: string
  title?: string
}

let isRefreshing = false
let refreshPromise: Promise<boolean> | null = null

async function tryRefreshTokens(): Promise<boolean> {
  if (isRefreshing && refreshPromise) {
    return refreshPromise
  }

  isRefreshing = true
  refreshPromise = fetch(`${URL}/auth/refresh`, {
    method: 'POST',
    credentials: 'include',
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
  return fetch(`${URL}${path}`, {
    ...init,
    credentials: 'include',
    headers: {
      'Content-Type': 'application/json',
      ...init?.headers,
    },
  })
}

export async function apiFetch<T>(
  path: string,
  init?: RequestInit,
): Promise<T> {
  try {
    let res = await executeRequest(path, init)
    1
    if (res.status === 401) {
      const refreshed = await tryRefreshTokens()

      if (!refreshed) {
        // window.location.href = '/login'
        throw new Error('Session expired, please log in again')
      }
      res = await executeRequest(path, init)

      if (res.status === 401) {
        // window.location.href = '/login'
        throw new Error('Session expired, please log in again')
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
