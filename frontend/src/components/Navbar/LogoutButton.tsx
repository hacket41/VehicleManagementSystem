import { useMutation } from '@tanstack/react-query'
import { useRouter } from '@tanstack/react-router'
import { toast } from 'sonner'
import { logout } from '#/api/auth.api'
import { useAuth } from '#/hooks/useAuth'
import { queryClient } from '#/lib/queryClient'
import { Button } from '../ui/button'

export default function LogoutButton() {
  const { user } = useAuth()
  const router = useRouter()
  const { mutate: logoutMutation } = useMutation({
    mutationFn: logout,
    onSuccess: () => {
      queryClient.invalidateQueries()
      router.navigate({ to: '/' })
      toast.success('Logged out successfully')
    },
  })
  return (
    <div className="flex justify-between items-center gap-4">
      <span>{user?.name}</span>
      <Button type="button" onClick={() => logoutMutation()}>
        Logout
      </Button>
    </div>
  )
}
