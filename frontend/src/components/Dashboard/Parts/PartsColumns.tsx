import { useMutation, useQueryClient } from '@tanstack/react-query'
import type { ColumnDef } from '@tanstack/react-table'
import { Copy, Edit, MoreVerticalIcon, Trash2, Truck } from 'lucide-react'
import { useState } from 'react'
import { toast } from 'sonner'
import { deleteImage, deletePart } from '#/api/parts.api'
import {
  AlertDialog,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogTitle,
} from '#/components/ui/alert-dialog'
import { Spinner } from '#/components/ui/spinner'
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
import RestockPartDialog from './RestockPartDialog'

export const PartsColumns: ColumnDef<Part>[] = [
  {
    accessorKey: 'imageUrl',
    header: 'Image',
    cell: ({ row }) => (
      <img
        src={row.original.imageUrl}
        alt="Part"
        className="h-12 w-12 object-cover"
      />
    ),
  },
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
    cell: ({ row }) => <PartActions part={row.original} />,
  },
]

interface PartActionsProps {
  part: Part
}

function PartActions({ part }: PartActionsProps) {
  const [open, setOpen] = useState(false)
  const [deleteOpen, setDeleteOpen] = useState(false)
  const [restockOpen, setRestockOpen] = useState(false)
  const queryClient = useQueryClient()

  const { mutate: deletePartMutate, isPending: isDeleting } = useMutation({
    mutationFn: async () => {
      if (part.imageUrl) {
        const fileKey = part.imageUrl.split('/f/')[1]
        if (fileKey) {
          const result = await deleteImage({ data: fileKey })
          if (!result) {
            toast.error('Failed to delete image')
          }
        }
      }

      await deletePart(part.id)
    },
    onSuccess: () => {
      toast.success('Part deleted successfully')
    },
    onError: () => {
      toast.error('Failed to delete part')
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ['parts'] })
      setDeleteOpen(false)
    },
  })

  return (
    <>
      <AddEditPartsDialog initialData={part} open={open} setOpen={setOpen} />
      <RestockPartDialog
        id={part.id}
        currentStock={part.stockQuantity}
        open={restockOpen}
        setOpen={setRestockOpen}
      />

      <AlertDialog open={deleteOpen} onOpenChange={setDeleteOpen}>
        <AlertDialogContent>
          <AlertDialogTitle>Delete Part</AlertDialogTitle>

          <AlertDialogDescription>
            Are you sure you want to delete this part?
          </AlertDialogDescription>

          <AlertDialogFooter>
            <Button onClick={() => setDeleteOpen(false)}>Cancel</Button>

            <Button
              variant="destructive"
              onClick={() => deletePartMutate()}
              disabled={isDeleting}
            >
              {isDeleting ? <Spinner /> : 'Delete'}
            </Button>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>

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
                navigator.clipboard.writeText(part.id ?? '')
                toast.info('Copied part ID to clipboard')
              }}
            >
              <Copy />
              Copy part ID
            </DropdownMenuItem>

            <DropdownMenuSeparator />

            <DropdownMenuItem onClick={() => setOpen(true)}>
              <Edit />
              Edit Part
            </DropdownMenuItem>

            <DropdownMenuItem onClick={() => setRestockOpen(true)}>
              <Truck />
              Restock Part
            </DropdownMenuItem>

            <DropdownMenuItem onClick={() => setDeleteOpen(true)}>
              <Trash2 />
              Delete Part
            </DropdownMenuItem>
          </DropdownMenuGroup>
        </DropdownMenuContent>
      </DropdownMenu>
    </>
  )
}
