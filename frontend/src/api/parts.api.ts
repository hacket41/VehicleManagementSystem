import { queryOptions } from '@tanstack/react-query'
import { apiFetch } from '#/lib/api'
import type { Part, PartCategory } from '#/types/parts.types'

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
