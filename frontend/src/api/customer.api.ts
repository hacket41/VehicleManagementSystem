import { queryOptions } from '@tanstack/react-query'
import { apiFetch } from '#/lib/api'
import type { CustomerSearchResult } from '#/types/customer.types'
 
export const searchCustomers = (query: string) => {
  return queryOptions({
    queryKey: ['customer-search', query],
    queryFn: async () =>
      apiFetch<CustomerSearchResult[]>(
        `/api/customersearch?q=${encodeURIComponent(query)}`,
        { method: 'GET' },
      ),
    enabled: query.trim().length > 0,
  })
}
 