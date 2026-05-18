import { useQuery } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { SearchIcon } from 'lucide-react'
import { useState } from 'react'
import { searchCustomers } from '#/api/customer.api'
import { DataTable } from '#/components/Dashboard/data-table'
import { CustomerSearchColumns } from '#/components/Customer/CustomerSearchColumns'
import { Input } from '#/components/ui/input'
import { Skeleton } from '#/components/ui/skeleton'

export const Route = createFileRoute('/_main/customers')(  {
  component: RouteComponent,
})

function RouteComponent() {
  const [query, setQuery] = useState('')
  const [debouncedQuery, setDebouncedQuery] = useState('')

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value
    setQuery(val)
    clearTimeout((handleChange as any)._t)
    ;(handleChange as any)._t = setTimeout(() => setDebouncedQuery(val), 400)
  }

  const { data, isFetching, isError } = useQuery({
    ...searchCustomers(debouncedQuery),
  })

  return (
    <div className="container mx-auto py-6 space-y-6">
      <div>
        <h1 className="text-lg font-semibold">Customer Search</h1>
        <p className="text-muted-foreground text-xs mt-0.5">
          Search by name, phone, ID, email, or vehicle number
        </p>
      </div>

      {/* Search Input */}
      <div className="relative max-w-md">
        <SearchIcon className="absolute left-2.5 top-1/2 -translate-y-1/2 size-3.5 text-muted-foreground pointer-events-none" />
        <Input
          className="pl-8"
          placeholder="e.g. Alice, 9800000000, BA 1 CHA 1234..."
          value={query}
          onChange={handleChange}
        />
      </div>

      {/* Results */}
      {isFetching && (
        <div className="space-y-2">
          {Array.from({ length: 4 }).map((_, i) => (
            <Skeleton key={i} className="h-10 w-full rounded-md" />
          ))}
        </div>
      )}

      {isError && (
        <p className="text-destructive text-sm">
          Something went wrong. Please try again.
        </p>
      )}

      {!isFetching && data && (
        <DataTable columns={CustomerSearchColumns} data={data} />
      )}

      {!isFetching && debouncedQuery && data?.length === 0 && (
        <p className="text-muted-foreground text-sm text-center py-10">
          No customers found for &quot;{debouncedQuery}&quot;
        </p>
      )}

      {!debouncedQuery && (
        <p className="text-muted-foreground text-sm text-center py-10">
          Start typing to search for customers
        </p>
      )}
    </div>
  )
}