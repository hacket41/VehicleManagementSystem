import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_main/test/test1')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_main/test/test1"!</div>
}
