import { queryOptions } from '@tanstack/react-query'
import { createServerFn } from '@tanstack/react-start'
import { apiFetch } from '#/lib/api'
import type {
  Part,
  PartCategory,
  RestockPartRequest,
} from '#/types/parts.types'
import { utapi } from './server/uploadthing'

export const getParts = () => {
  return queryOptions({
    queryKey: ['parts'],
    queryFn: async () =>
      apiFetch<Part[]>('/api/part', {
        method: 'GET',
      }),
  })
}
export const getPart = (id: string) => {
  return queryOptions({
    queryKey: ['part', id],
    queryFn: async () =>
      apiFetch<Part>(`/api/part/${id}`, {
        method: 'GET',
      }),
  })
}

export const postPart = async (data: Part) => {
  await apiFetch<Part>('/api/part', {
    method: 'POST',
    body: data,
  })
}

export const putPart = async (id: string, data: Part) => {
  await apiFetch<Part>(`/api/part/${id}`, {
    method: 'PUT',
    body: data,
  })
}

export const deletePart = async (id: string) => {
  await apiFetch(`/api/part/${id}`, {
    method: 'DELETE',
  })
}

export const restockPart = async (payload: RestockPartRequest) => {
  return await apiFetch(`/api/part/restock`, {
    method: 'POST',
    body: payload,
  })
}

export const getPartsCategories = () => {
  return queryOptions({
    queryKey: ['parts-categories'],
    queryFn: async () =>
      apiFetch<PartCategory[]>('/api/part/categories', {
        method: 'GET',
      }),
  })
}

export const postCategory = async (data: PartCategory) => {
  await apiFetch<PartCategory>('/api/part/categories', {
    method: 'POST',
    body: data,
  })
}

export const putCategory = async (id: string, data: PartCategory) => {
  await apiFetch<PartCategory>(`/api/part/categories/${id}`, {
    method: 'PUT',
    body: data,
  })
}

export const deleteCategory = async (id: string) => {
  await apiFetch<PartCategory>(`/api/part/categories/${id}`, {
    method: 'DELETE',
  })
}

export const deleteImage = createServerFn({ method: 'POST' })
  .inputValidator((imageId: string) => imageId)
  .handler(async ({ data: imageId }) => {
    const res = await utapi.deleteFiles(imageId)
    return res.success
  })
