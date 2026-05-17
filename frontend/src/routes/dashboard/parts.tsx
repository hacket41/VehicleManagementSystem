import { useSuspenseQuery } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { getParts, getPartsCategories } from '#/api/parts.api'
import { getVendors } from '#/api/vendor.api'
import { DataTable } from '#/components/Dashboard/data-table'
import AddEditPartsDialog from '#/components/Dashboard/Parts/AddEditPartsDialog'
import { PartsColumns } from '#/components/Dashboard/Parts/PartsColumns'
import { CenteredSpinner } from '#/components/ui/spinner'

export const Route = createFileRoute('/dashboard/parts')({
  loader: ({ context }) => {
    context.queryClient.ensureQueryData(getParts())
    context.queryClient.ensureQueryData(getPartsCategories())
    context.queryClient.ensureQueryData(getVendors())
  },
  component: RouteComponent,
  pendingMs: 0,
  pendingComponent: () => <CenteredSpinner className="size-16" />,
})

function RouteComponent() {
  const { data: parts } = useSuspenseQuery(getParts())

  return (
    <div className="container mx-auto py-10">
      <section className="flex justify-end pb-4">
        <AddEditPartsDialog />
      </section>
      <DataTable columns={PartsColumns} data={parts} />
    </div>
  )
}
