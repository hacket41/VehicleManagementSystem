import { queryOptions } from '@tanstack/react-query'
import { apiFetch } from '#/lib/api'
import type { Vendor } from '#/types/vendor.types'

export const getVendors = () => {
  return queryOptions({
    queryKey: ['vendors'],
    queryFn: async () => {
      const data = await apiFetch<Vendor[]>('/api/vendor', {
        method: 'GET',
      })
      return data
    },
  })
}
