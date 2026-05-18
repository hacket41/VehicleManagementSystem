export interface VehicleSummary {
  id: number
  vehicleNumber: string
  make: string
  model: string
  year: number
}
 
export interface CustomerSearchResult {
  id: string
  name: string
  email: string
  phoneNumber: string
  address: string
  vehicles: VehicleSummary[]
}
 