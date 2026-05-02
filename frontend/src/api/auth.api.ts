import { apiFetch } from '#/lib/api'

export const logout = async () => {
  return apiFetch('/api/auth/logout', {
    method: 'POST',
    // body: data,
  })
}
