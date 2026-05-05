import { useMutation } from '@tanstack/react-query'
import { useRouter } from '@tanstack/react-router'
import { useState } from 'react'
import { toast } from 'sonner'
import { logout } from '#/api/auth.api'
import { queryClient } from '#/lib/queryClient'
import { useAuth } from '#/providers/AuthContextProvider'
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from '../ui/alert-dialog'
import { Button } from '../ui/button'
import { Spinner } from '../ui/spinner'

export default function LogoutButton() {
  const [open, setOpen] = useState(false)
  const { user } = useAuth()
  const router = useRouter()
  const { mutate: logoutMutation, isPending } = useMutation({
    mutationFn: logout,
    onSuccess: () => {
      toast.success('Logged out successfully')
      queryClient.setQueryData(['me'], null)
      setOpen(false)
      router.navigate({ to: '/' })

      // window.location.href = '/'
    },
    onError: (e) => toast.error(e.message),
  })
  return (
    <div className="flex justify-between items-center gap-4">
      <span>{user?.name}</span>
      <AlertDialog open={open} onOpenChange={setOpen}>
        <AlertDialogTrigger>
          <Button type="button" disabled={isPending}>
            Logout
          </Button>
        </AlertDialogTrigger>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Logout</AlertDialogTitle>
            <AlertDialogDescription>
              Are you sure you want to logout?
            </AlertDialogDescription>
          </AlertDialogHeader>

          <AlertDialogFooter>
            <AlertDialogCancel>Cancel</AlertDialogCancel>
            <AlertDialogAction
              type="button"
              variant="destructive"
              onClick={() => logoutMutation()}
              disabled={isPending}
            >
              {isPending ? <Spinner /> : 'Confirm'}
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </div>
  )
}
