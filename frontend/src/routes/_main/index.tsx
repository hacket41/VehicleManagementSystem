import { useMutation } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { toast } from 'sonner'
import { refreshToken } from '#/api/auth.api'
import { Button } from '#/components/ui/button'

export const Route = createFileRoute('/_main/')({
  component: App,
})

function App() {
  const { mutate: refreshTokens } = useMutation({
    mutationFn: refreshToken,
    onSuccess: () => {
      toast.success('Token refreshed')
    },
    onError: () => {
      toast.error('Failed to refresh token')
    },
  })
  return (
    <main className="container mx-auto p-8 space-y-6">
      <Button onClick={() => refreshTokens()}>Refresh Token</Button>
    </main>
  )
}
