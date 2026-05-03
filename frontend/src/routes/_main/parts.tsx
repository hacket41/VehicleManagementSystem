import { useQuery } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { getParts } from '#/api/parts.api'
import PartCard from '#/components/Parts/PartCard'
import { queryClient } from '#/lib/queryClient'

export const Route = createFileRoute('/_main/parts')({
  component: RouteComponent,
  loader: async () => await queryClient.ensureQueryData(getParts()),
})

function RouteComponent() {
  const { data: parts } = useQuery(getParts())
  if (!parts) return null
  console.log(parts)
  return (
    <div>
      <div>
        <h1>All Parts</h1>
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
          {parts.map((part) => (
            <PartCard key={part.id} part={part} />
          ))}
        </div>
      </div>
    </div>
  )
}
