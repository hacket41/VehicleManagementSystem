import { queryOptions } from '@tanstack/react-query'
import { apiFetch } from '#/lib/api'
import type { MeResponse } from '#/routes/_main'

export const getMe = () => {
  return queryOptions({
    queryKey: ['me'],
    queryFn: () => apiFetch<MeResponse>('/api/user/me'),
  })
}

export const logout = async () => {
  return apiFetch('/api/auth/logout', {
    method: 'POST',
  })
}
