import { useMutation } from '@tanstack/react-query'
import { useRouter } from '@tanstack/react-router'
import { toast } from 'sonner'
import { logout } from '#/api/auth.api'
import { queryClient } from '#/lib/queryClient'
import { Button } from '../ui/button'

export default function LogoutButton() {
  const router = useRouter()
  const { mutate: logoutMutation } = useMutation({
    mutationFn: logout,
    onSuccess: () => {
      // router.invalidate()
      queryClient.invalidateQueries()
      router.navigate({ to: '/' })
      toast.success('Logged out successfully')
    },
  })
  return (
    <Button type="button" onClick={() => logoutMutation()}>
      Logout
    </Button>
  )
}
