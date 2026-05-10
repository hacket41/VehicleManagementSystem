'use client'

import type { ColumnDef } from '@tanstack/react-table'
import type { Part } from '#/types/parts.types'

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
  },
]
