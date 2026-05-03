import { queryOptions } from '@tanstack/react-query'
import { apiFetch } from '#/lib/api'
import type { Part } from '#/types/parts.types'

export const getParts = () => {
  return queryOptions({
    queryKey: ['parts'],
    queryFn: async () =>
      apiFetch<Part[]>('/api/part/all', {
        method: 'GET',
      }),
  })
}

// export const
