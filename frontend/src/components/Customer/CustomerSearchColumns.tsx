import type { ColumnDef } from '@tanstack/react-table'
import { Badge } from '#/components/ui/badge'
import type { CustomerSearchResult } from '#/types/customer.types'

export const CustomerSearchColumns: ColumnDef<CustomerSearchResult>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
  },
  {
    accessorKey: 'email',
    header: 'Email',
  },
  {
    accessorKey: 'phoneNumber',
    header: 'Phone',
  },
  {
    accessorKey: 'address',
    header: 'Address',
    cell: ({ row }) => row.original.address || <span className="text-muted-foreground">—</span>,
  },
  {
    accessorKey: 'vehicles',
    header: 'Vehicles',
    cell: ({ row }) => {
      const vehicles = row.original.vehicles
      if (!vehicles.length)
        return <span className="text-muted-foreground">No vehicles</span>
      return (
        <div className="flex flex-wrap gap-1">
          {vehicles.map((v) => (
            <Badge key={v.id} variant="outline">
              {v.vehicleNumber} · {v.make} {v.model} ({v.year})
            </Badge>
          ))}
        </div>
      )
    },
  },
]