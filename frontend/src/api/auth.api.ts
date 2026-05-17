import { queryOptions } from '@tanstack/react-query'
import type { LoginPayload } from '#/components/Auth/LoginForm'
import type { RegisterPayload } from '#/components/Auth/SignupForm'
import { apiFetch } from '#/lib/api'
import type { MeResponse } from '#/types/user.types'

// const getMeServerAuth = createServerFn().handler(async () => {
//   try {
//     return await apiFetch<MeResponse>('/api/user/me', { method: 'GET' })
//   } catch (error) {
//     if (error instanceof ApiError && error.status === 401) {
//       return null
//     }
//     throw error
//   }
// })
//

export const getMe = () => {
  return queryOptions({
    queryKey: ['me'],
    queryFn: async () => {
      return await apiFetch<MeResponse>('/api/user/me', { method: 'GET' })
    },
    staleTime: Infinity,
    retry: false,
  })
}

export const registerCustomer = async (data: RegisterPayload) => {
  return apiFetch('/api/auth/register-customer', {
    method: 'POST',
    body: data,
  })
}

export const login = async (data: LoginPayload) => {
  return apiFetch('/api/auth/login', {
    method: 'POST',
    body: data,
  })
}

export const logout = async () => {
  return apiFetch('/api/auth/logout', {
    method: 'POST',
  })
}
