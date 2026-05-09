import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/dashboard/vehicles')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/dashboard/vehicles"!</div>
}
