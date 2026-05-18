import { useQuery } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { getParts } from '#/api/parts.api'
import PartCard from '#/components/Parts/PartCard'

export const Route = createFileRoute('/_main/parts')({
  component: RouteComponent,
  loader: ({ context }) => context.queryClient.ensureQueryData(getParts()),
})

function RouteComponent() {
  const { data: parts } = useQuery(getParts())
  if (!parts) return null
  // console.log(parts)
  return (
    <div>
      <div className="flex justify-center items-center p-8">
        <h1 className="font-semibold text-2xl">All Parts</h1>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-8">
        {parts.map((part) => (
          <PartCard key={part.id} part={part} />
        ))}
      </div>
    </div>
  )
}
