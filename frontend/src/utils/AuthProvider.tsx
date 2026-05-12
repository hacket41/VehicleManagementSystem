import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { createContext, useContext } from 'react'
import { getMe } from '#/api/auth.api'
import { apiFetch } from '#/lib/api'
import type { MeResponse } from '#/types/user.types'

export interface AuthState {
  isAuthenticated: boolean
  user: MeResponse | null
  isAdmin: boolean
  isStaff: boolean
  isPending: boolean
}

const AuthContext = createContext<AuthState | undefined>(undefined)

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const { data, isPending } = useQuery({ ...getMe(), retry: false })
  // console.log(data)

  const value: AuthState = {
    isAuthenticated: !!data,
    user: data ?? null,
    isAdmin: data?.roles.includes('Admin') ?? false,
    isStaff: data?.roles.includes('Staff') ?? false,
    isPending,
  }

  // if (isPending) return <Spinner />
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) throw new Error('useAuth must be used within an AuthProvider')
  return context
}

// export function useLogout() {
//   const queryClient = useQueryClient()
//   return useMutation({
//     mutationFn: async () => {
//       return apiFetch('/api/auth/logout', {
//         method: 'POST',
//       })
//     },
//     onSuccess: () => {
//       queryClient.setQueryData(['me'], null)
//     },
//   })
// }
