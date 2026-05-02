import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_main/test/')({ component: TestPage })

function TestPage() {
  return <div>index</div>
}
