import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_main/testing')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_main/testing"!</div>
}
