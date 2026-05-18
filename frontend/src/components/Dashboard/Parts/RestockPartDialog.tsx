import { useMutation, useQueryClient } from '@tanstack/react-query'
import { Minus, Plus } from 'lucide-react'
import { useState } from 'react'
import { toast } from 'sonner'
import { restockPart } from '#/api/parts.api'
import { Button } from '#/components/ui/button'
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '#/components/ui/dialog'
import { Input } from '#/components/ui/input'
import { Separator } from '#/components/ui/separator'
import { Spinner } from '#/components/ui/spinner'
import type { RestockPartRequest } from '#/types/parts.types'

interface RestockPartDialogProps {
  open: boolean
  setOpen: (open: boolean) => void
  id: string
  currentStock: number
}

export default function RestockPartDialog({
  open,
  setOpen,
  id,
  currentStock,
}: RestockPartDialogProps) {
  const [stockQuantity, setStockQuantity] = useState<number>(0)

  const queryClient = useQueryClient()

  const { mutate, isPending } = useMutation({
    mutationFn: (payload: RestockPartRequest) => restockPart(payload),

    onSuccess: (res) => toast.success(`New Stock: ${res}`),
    onError: (err) => toast.error(`Failed to restock part: ${err}`),
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ['parts'] })
      setOpen(false)
    },
  })

  const onSubmit = () => {
    const payload: RestockPartRequest = { id: id, stockQuantity: stockQuantity }
    console.log(payload)
    mutate(payload)
  }
  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Restock Part</DialogTitle>
          <DialogDescription>Current stock: {currentStock}</DialogDescription>
        </DialogHeader>
        <div className="flex justify-center">
          <div className="flex gap-4 justify-between items-center">
            <Button
              variant={'default'}
              size="icon-sm"
              onClick={() => setStockQuantity((prev) => Math.max(1, prev - 5))}
            >
              <Minus />
            </Button>
            <Input
              type="number"
              value={stockQuantity}
              min={0}
              className="w-15"
            />

            <Button
              variant={'default'}
              size="icon-sm"
              onClick={() =>
                setStockQuantity((prev) => Math.min(200, prev + 5))
              }
            >
              <Plus />
            </Button>
          </div>
        </div>
        <Separator />
        <DialogFooter>
          <DialogClose
            render={
              <Button variant={'secondary'} disabled={isPending}>
                Close
              </Button>
            }
          ></DialogClose>
          <Button variant="default" onClick={onSubmit} disabled={isPending}>
            {isPending ? <Spinner /> : 'Restock'}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
