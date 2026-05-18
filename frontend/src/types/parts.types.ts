export interface Part {
  id: string
  name: string
  partNumber: string
  description: string
  vendorId: number
  vendorName: string
  categoryId: number
  categoryName: string
  compatibleVehicles: string
  costPrice: number
  sellingPrice: number
  stockQuantity: number
  imageUrl: string
  isActive: boolean
  updatedAt: string
}

// export interface PartCreateRequest {
//   name: string
//   partNumber: string
//   description: string
//   categoryId: string
//   vendorId: string
//   compatibleVehicles: string
//   costPrice: number
//   sellingPrice: number
//   stockQuantity: number
//   isActive: boolean
// }

export interface PartCategory {
  id?: string
  name: string
}

export interface RestockPartRequest {
  id: string
  stockQuantity: number
}
