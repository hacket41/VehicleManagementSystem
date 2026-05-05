export interface Part {
  id?: number
  name: string
  partNumber: string
  description: string
  vendorName: string
  categoryName: string
  compatibleVehicle: string
  sellingPrice: number
  stockQuantity: number
  updatedAt?: string
}
