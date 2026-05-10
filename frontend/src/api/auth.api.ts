import { queryOptions } from '@tanstack/react-query'
import { createServerFn } from '@tanstack/react-start'
import type { LoginPayload } from '#/components/Auth/LoginForm'
import type { RegisterPayload } from '#/components/Auth/SignupForm'
import { apiFetch } from '#/lib/api'
import type { MeResponse } from '#/types/user.types'

const getMeServerAuth = createServerFn().handler(async () => {
  try {
    return await apiFetch<MeResponse>('/api/user/me', { method: 'GET' })
  } catch (error) {
    if (
      typeof error === 'object' &&
      error !== null &&
      'status' in error &&
      (error as { status: number }).status === 401
    ) {
      return null
    }
    throw error
  }
})

export const getMe = () => {
  return queryOptions({
    queryKey: ['me'],
    queryFn: () => getMeServerAuth(),
    staleTime: 1000 * 60 * 5,
    retry: false,
  })
}

export const refreshToken = async () => {
  return apiFetch('/api/auth/refresh-token', {
    method: 'POST',
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
