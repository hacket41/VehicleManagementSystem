import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { ErrorComponent } from '@tanstack/react-router'
import { PlusCircle, X } from 'lucide-react'
import { useState } from 'react'
import { Controller, useForm } from 'react-hook-form'
import { toast } from 'sonner'
import { getPartsCategories, postPart, putPart } from '#/api/parts.api'
import { getVendors } from '#/api/vendor.api'
import { Button } from '#/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '#/components/ui/dialog'
import {
  Field,
  FieldDescription,
  FieldError,
  FieldGroup,
  FieldLabel,
  FieldSeparator,
  FieldSet,
} from '#/components/ui/field'
import { Input } from '#/components/ui/input'
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectSeparator,
  SelectTrigger,
  SelectValue,
} from '#/components/ui/select'
import { Spinner } from '#/components/ui/spinner'
import { Switch } from '#/components/ui/switch'
import { Textarea } from '#/components/ui/textarea'
import type { Part } from '#/types/parts.types'
import { UploadDropzone } from '#/utils/uploadthing'

interface AddEditPartsDialogProps {
  initialData?: Part
  open?: boolean
  setOpen?: (open: boolean) => void
}

export default function AddEditPartsDialog({
  initialData,
  open: controlledOpen,
  setOpen: controlledSetOpen,
}: AddEditPartsDialogProps) {
  const [isUploading, setIsUploading] = useState(false)

  const [internalOpen, setInternalOpen] = useState(false)
  const queryClient = useQueryClient()

  const isControlled = controlledOpen !== undefined
  const open = isControlled ? controlledOpen : internalOpen
  const setOpen = isControlled ? controlledSetOpen : setInternalOpen

  const {
    data: partsCategories,
    isPending: isPartsCategoriesPending,
    isError: isPartsCategoriesError,
    error: partsCategoriesError,
  } = useQuery(getPartsCategories())

  const {
    data: vendors,
    isPending: isVendorsPending,
    isError: isVendorsError,
    error: vendorsError,
  } = useQuery(getVendors())

  const {
    register,
    handleSubmit,
    formState: { errors },
    control,
    reset,
  } = useForm<Part>({
    defaultValues: {
      name: initialData?.name ?? '',
      partNumber: initialData?.partNumber ?? '',
      description: initialData?.description ?? '',
      vendorId: initialData?.vendorId ?? 0,
      categoryId: initialData?.categoryId ?? 0,
      compatibleVehicles: initialData?.compatibleVehicles ?? '',
      costPrice: initialData?.costPrice ?? 0,
      sellingPrice: initialData?.sellingPrice ?? 0,
      stockQuantity: initialData?.stockQuantity ?? 0,
      isActive: initialData?.isActive ?? true,
      imageUrl: initialData?.imageUrl ?? '',
    },
  })

  const {
    mutate: addPartMutation,
    isPending: isAddPartPending,
    // isError: isAddPartError,
    // error: addPartError,
  } = useMutation({
    mutationFn: postPart,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['parts'] })
      toast.success('Part added successfully')
      reset()
      if (setOpen) setOpen(false)
    },
    onError: (error) => {
      toast.error(error.message)
    },
  })

  const {
    mutate: editPartMutation,
    isPending: isEditPartPending,
    // isError: isEditPartError,
    // error: editPartError,
  } = useMutation({
    mutationFn: (data: Part) => putPart(initialData?.id ?? '', data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['parts'] })
      toast.success('Part updated successfully')
      reset()
      if (setOpen) setOpen(false)
    },
    onError: (error) => {
      toast.error(error.message)
    },
  })
  const isSubmitting = isAddPartPending || isEditPartPending || isUploading

  function onSubmit(data: Part) {
    console.log(data)
    if (initialData) {
      editPartMutation(data)
    } else {
      addPartMutation(data)
    }
  }
  function handleOpenChange(newOpen: boolean) {
    if (setOpen) setOpen(newOpen)
  }

  const isError = isPartsCategoriesError || isVendorsError
  const error = partsCategoriesError || vendorsError

  if (isError) return <ErrorComponent error={error} />

  return (
    <Dialog open={open} onOpenChange={handleOpenChange}>
      {!initialData && (
        <DialogTrigger
          render={
            <Button size={'lg'}>
              <PlusCircle className="size-4 mr-2" />
              Add Part
            </Button>
          }
        />
      )}
      <DialogContent className="max-w-4xl!">
        <DialogHeader>
          <DialogTitle>Add Part</DialogTitle>
          <DialogDescription></DialogDescription>
        </DialogHeader>
        <form onSubmit={handleSubmit(onSubmit)}>
          <FieldGroup>
            <FieldSet>
              <FieldDescription>
                All transactions are secure and encrypted
              </FieldDescription>
              <div className="flex gap-4 justify-end items-end">
                <FieldLabel htmlFor="isActive">Active</FieldLabel>
                <Controller
                  name="isActive"
                  control={control}
                  render={({ field }) => (
                    <Switch
                      id="isActive"
                      checked={field.value}
                      onCheckedChange={field.onChange}
                    />
                  )}
                />
              </div>
              <FieldGroup>
                <FieldGroup className="grid grid-cols-2">
                  <Field>
                    <FieldLabel htmlFor="name">Part Name</FieldLabel>
                    <Input
                      id="name"
                      type="text"
                      placeholder="NGK Spark Plugs"
                      {...register('name', {
                        required: 'Name cannot be empty',
                      })}
                    />
                    <FieldDescription>Enter the part name</FieldDescription>
                    {errors.name && (
                      <FieldError>{errors.name.message}</FieldError>
                    )}
                  </Field>
                  <Field>
                    <FieldLabel htmlFor="partNumber">Part Number</FieldLabel>
                    <Input
                      type="text"
                      id="partNumber"
                      placeholder="1234 5678 9012 3456"
                      {...register('partNumber', {
                        required: 'Part number cannot be empty',
                      })}
                    />
                    <FieldDescription>Enter the part number</FieldDescription>
                    {errors.partNumber && (
                      <FieldError>{errors.partNumber.message}</FieldError>
                    )}
                  </Field>
                </FieldGroup>

                <Field>
                  <FieldLabel htmlFor="description">
                    Part Description
                  </FieldLabel>
                  <Textarea
                    id="description"
                    placeholder="Part description"
                    {...register('description', {
                      required: 'Description cannot be empty',
                    })}
                  />
                  <FieldDescription>
                    Enter the part description
                  </FieldDescription>
                  {errors.description && (
                    <FieldError>{errors.description.message}</FieldError>
                  )}
                </Field>

                <div className="grid grid-cols-2 gap-4">
                  <Controller
                    control={control}
                    name="categoryId"
                    rules={{ required: 'Category is required' }}
                    render={({ field, fieldState }) => (
                      <Field data-invalid={fieldState.invalid}>
                        <FieldLabel htmlFor="categoryId">
                          Part Category
                        </FieldLabel>
                        {isPartsCategoriesPending ? (
                          <Spinner />
                        ) : (
                          <Select
                            name={field.name}
                            value={field.value}
                            onValueChange={field.onChange}
                            items={partsCategories.map((c) => ({
                              label: c.name,
                              value: c.id,
                            }))}
                          >
                            <SelectTrigger
                              id="categoryId"
                              aria-invalid={fieldState.invalid}
                            >
                              <SelectValue placeholder="Select a category" />
                            </SelectTrigger>
                            <SelectContent>
                              <SelectGroup>
                                <SelectLabel>Part Category</SelectLabel>
                                <SelectSeparator />
                                {partsCategories?.map((category) => (
                                  <SelectItem
                                    key={category.id}
                                    value={category.id}
                                  >
                                    {category.name}
                                  </SelectItem>
                                ))}
                              </SelectGroup>
                            </SelectContent>
                          </Select>
                        )}

                        <FieldDescription className=" ml-1">
                          Select a part category
                        </FieldDescription>
                        {errors.categoryId && (
                          <FieldError>{errors.categoryId.message}</FieldError>
                        )}
                      </Field>
                    )}
                  />
                  <Controller
                    control={control}
                    name="vendorId"
                    rules={{ required: 'Vendor is Required' }}
                    render={({ field, fieldState }) => (
                      <Field>
                        <FieldLabel htmlFor="vendorId">Vendor</FieldLabel>

                        {isVendorsPending ? (
                          <Spinner />
                        ) : (
                          <Select
                            name={field.name}
                            value={field.value}
                            onValueChange={field.onChange}
                            items={vendors.map((v) => ({
                              label: v.name,
                              value: v.id,
                            }))}
                          >
                            <SelectTrigger
                              id="vendorId"
                              aria-invalid={fieldState.invalid}
                            >
                              <SelectValue placeholder="Select a vendor" />
                            </SelectTrigger>

                            <SelectContent>
                              <SelectGroup>
                                <SelectLabel>Vendors</SelectLabel>

                                {vendors?.map((vendor) => (
                                  <SelectItem key={vendor.id} value={vendor.id}>
                                    {vendor.name}
                                  </SelectItem>
                                ))}
                              </SelectGroup>
                            </SelectContent>
                          </Select>
                        )}

                        <FieldDescription className="ml-1">
                          Select a vendor
                        </FieldDescription>

                        {errors.vendorId && (
                          <FieldError>{errors.vendorId.message}</FieldError>
                        )}
                      </Field>
                    )}
                  />
                </div>
              </FieldGroup>
            </FieldSet>
            <FieldSeparator />
            <FieldSet>
              <FieldGroup className="grid grid-cols-3">
                <Field>
                  <FieldLabel htmlFor="costPrice">Cost Price</FieldLabel>
                  <Input
                    id="costPrice"
                    type="number"
                    min={0}
                    placeholder="Cost Price"
                    {...register('costPrice', {
                      required: 'Cost Price is required',
                    })}
                  />
                  <FieldDescription>
                    Enter the cost price of the part.
                  </FieldDescription>
                  {errors.costPrice && (
                    <FieldError>{errors.costPrice.message}</FieldError>
                  )}
                </Field>
                <Field>
                  <FieldLabel htmlFor="sellingPrice">Selling Price</FieldLabel>
                  <Input
                    id="sellingPrice"
                    type="number"
                    min={0}
                    placeholder="Selling Price"
                    {...register('sellingPrice', {
                      required: 'Selling Price is required',
                    })}
                  />
                  <FieldDescription>
                    Enter the selling price of the part.
                  </FieldDescription>
                  {errors.sellingPrice && (
                    <FieldError>{errors.sellingPrice.message}</FieldError>
                  )}
                </Field>
                <Field>
                  <FieldLabel htmlFor="stockQuantity">
                    Stock Quantity
                  </FieldLabel>
                  <Input
                    id="stockQuantity"
                    type="number"
                    placeholder="Stock Quantity"
                    {...register('stockQuantity', {
                      required: 'Stock Quantity is required',
                    })}
                  />
                  <FieldDescription>
                    Enter the stock quantity of the part.
                  </FieldDescription>
                  {errors.stockQuantity && (
                    <FieldError>{errors.stockQuantity.message}</FieldError>
                  )}
                </Field>
              </FieldGroup>
              <Controller
                control={control}
                name="imageUrl"
                render={({ field }) => (
                  <Field>
                    <FieldLabel>Part Image</FieldLabel>
                    {field.value ? (
                      <div className="relative w-full rounded-md border overflow-hidden">
                        <img
                          src={field.value}
                          alt="Part preview"
                          className="w-full h-48 object-contain bg-muted"
                        />
                        <Button
                          type="button"
                          variant="outline"
                          size="icon"
                          className="absolute top-2 right-2 size-7"
                          onClick={() => field.onChange('')}
                        >
                          <X className="size-4" />
                        </Button>
                      </div>
                    ) : (
                      <UploadDropzone
                        endpoint="imageUploader"
                        onUploadBegin={() => setIsUploading(true)}
                        onClientUploadComplete={(res) => {
                          setIsUploading(false)
                          const url = res[0]?.ufsUrl
                          if (url) {
                            field.onChange(url)
                            toast.success('Image uploaded successfully')
                          }
                        }}
                        onUploadError={(error) => {
                          setIsUploading(false)
                          toast.error(`Upload failed: ${error.message}`)
                        }}
                      />
                    )}
                  </Field>
                )}
              />
            </FieldSet>
            <DialogFooter>
              <Button type="submit" disabled={isSubmitting}>
                {isSubmitting ? <Spinner /> : 'Submit'}
              </Button>
              <Button variant="outline" type="button" disabled={isSubmitting}>
                Cancel
              </Button>
            </DialogFooter>
          </FieldGroup>
        </form>
      </DialogContent>
    </Dialog>
  )
}
