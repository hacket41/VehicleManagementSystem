import { QueryClient } from '@tanstack/react-query'

// single QueryClient instance for the entire app
export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      refetchOnReconnect: false,
      retry: 1,
      staleTime: 1000 * 60 * 10,
      gcTime: 1000 * 60 * 10,
    },
    mutations: {
      retry: false,
    },
  },
})
