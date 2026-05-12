import { createFileRoute, redirect } from '@tanstack/react-router'
import { getMe } from '#/api/auth.api'

export const Route = createFileRoute('/dashboard/')({
  component: RouteComponent,
  beforeLoad: async ({ context }) => {
    try {
      const user = await context.queryClient.fetchQuery(getMe())
      const hasAccess =
        user.roles.includes('Admin') || user.roles.includes('Staff')
      if (!hasAccess) {
        throw redirect({ to: '/login' })
      }
    } catch {
      throw redirect({ to: '/login' })
    }
  },
})

function RouteComponent() {
  return (
    <div>
      <h1>Hellooo</h1>
      <h2>test</h2>
    </div>
  )
}
