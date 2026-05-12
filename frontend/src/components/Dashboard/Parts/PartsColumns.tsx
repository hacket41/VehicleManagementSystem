'use client'

import type { ColumnDef } from '@tanstack/react-table'
import { Copy, Edit, MoreVerticalIcon, Trash2 } from 'lucide-react'
import { useState } from 'react'
import { toast } from 'sonner'
import type { Part } from '#/types/parts.types'
import { Button } from '../../ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '../../ui/dropdown-menu'
import AddEditPartsDialog from './AddEditPartsDialog'

export const PartsColumns: ColumnDef<Part>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
  },
  {
    accessorKey: 'partNumber',
    header: 'Part Number',
  },
  {
    accessorKey: 'description',
    header: 'Description',
  },
  {
    accessorKey: 'vendorName',
    header: 'Vendor Name',
  },
  {
    accessorKey: 'categoryName',
    header: 'Category Name',
  },
  {
    accessorKey: 'compatibleVehicle',
    header: 'Compatible Vehicle',
  },
  {
    accessorKey: 'sellingPrice',
    header: 'Selling Price',
  },
  {
    accessorKey: 'stockQuantity',
    header: 'Stock Quantity',
  },
  {
    accessorKey: 'updatedAt',
    header: 'Updated At',
    cell: ({ row }) => {
      const date = new Date(row?.original?.updatedAt ?? '')
      return new Intl.DateTimeFormat('en-CA').format(date)
    },
  },
  {
    id: 'actions',
    cell: ({ row }) => {
      const [open, setOpen] = useState(false)
      // const { data: part } = useQuery(getPart(row.original.id ?? ''))
      //
      //   const { } = useMutation({
      //       mutationFn:
      // })
      return (
        <>
          <AddEditPartsDialog
            initialData={row.original}
            open={open}
            setOpen={setOpen}
          />
          <DropdownMenu>
            <DropdownMenuTrigger
              render={
                <Button variant="outline">
                  <MoreVerticalIcon />
                </Button>
              }
            />

            <DropdownMenuContent align="end">
              <DropdownMenuGroup>
                <DropdownMenuLabel>Actions</DropdownMenuLabel>
                <DropdownMenuItem
                  onClick={() => {
                    navigator.clipboard.writeText(row.original.id ?? '')
                    toast.info('Copied part ID to clipboard')
                  }}
                >
                  <Copy />
                  Copy part ID
                </DropdownMenuItem>
                <DropdownMenuSeparator />
                <DropdownMenuItem onClick={() => setOpen(true)}>
                  <Edit /> Edit Parts
                </DropdownMenuItem>
                <DropdownMenuItem>
                  <Trash2 /> Delete Part
                </DropdownMenuItem>
              </DropdownMenuGroup>
            </DropdownMenuContent>
          </DropdownMenu>
        </>
      )
    },
  },
]
