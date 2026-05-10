import { useQuery } from '@tanstack/react-query'
import { createFileRoute, ErrorComponent } from '@tanstack/react-router'
import { getParts } from '#/api/parts.api'
import { DataTable } from '#/components/Dashboard/data-table'
import { PartsColumns } from '#/components/Dashboard/PartsColumns'
import { Spinner } from '#/components/ui/spinner'

export const Route = createFileRoute('/dashboard/parts')({
  loader: ({ context }) => {
    context.queryClient.ensureQueryData(getParts())
  },
  component: RouteComponent,
})

function RouteComponent() {
  const { data: parts, isPending, isError, error } = useQuery(getParts())
  if (isPending) return <Spinner className="size-10" />
  if (isError) return <ErrorComponent error={error} />

  return (
    <div className="container mx-auto py-10">
      <DataTable columns={PartsColumns} data={parts} />
    </div>
  )
}
