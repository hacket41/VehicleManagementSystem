import { createFileRoute } from '@tanstack/react-router'
import { SignupForm } from '#/components/Auth/SignupForm'

export const Route = createFileRoute('/_main/signup')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <div className="flex p-20 items-center justify-center">
      <div className="flex flex-col items-center gap-4 w-full max-w-sm">
        <h3 className="text-xl font-semibold">Signup</h3>
        <SignupForm />
      </div>
    </div>
  )
}
