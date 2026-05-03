import { useQuery } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { getParts } from '#/api/parts.api'
import { queryClient } from '#/lib/queryClient'

export const Route = createFileRoute('/_main/parts')({
  component: RouteComponent,
  loader: async () => await queryClient.ensureQueryData(getParts()),
})

function RouteComponent() {
  const { data: parts } = useQuery(getParts())
  console.log(parts)
  return (
    <div>
      <div>
        <h1>All Parts</h1>
      </div>
    </div>
  )
}
