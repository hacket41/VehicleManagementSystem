import { useQuery } from '@tanstack/react-query'
import { createContext, useContext } from 'react'
import { getMe } from '#/api/auth.api'
import type { MeResponse } from '#/routes/_main'

type AuthContextType = {
  user: MeResponse | null
  isAuthenticated: boolean
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const { data: user } = useQuery(getMe())
  return (
    <AuthContext.Provider
      value={{ user: user ?? null, isAuthenticated: !!user }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }
  return context
}
