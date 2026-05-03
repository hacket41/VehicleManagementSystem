import { useQuery } from '@tanstack/react-query'
import { getMe } from '#/api/auth.api'

export function useAuth() {
  const { data: user, isPending, isError } = useQuery(getMe())
  return { user, isPending, isError }
}
