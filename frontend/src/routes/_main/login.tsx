import { createFileRoute } from '@tanstack/react-router'
import { LoginForm } from '#/components/Auth/LoginForm'

export const Route = createFileRoute('/_main/login')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <div className="flex p-20 items-center justify-center">
      <div className="flex flex-col items-center gap-4 w-full max-w-sm">
        <h3 className="text-xl font-semibold">Login</h3>
        <LoginForm />
      </div>
    </div>
  )
}
