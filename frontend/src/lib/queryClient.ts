import { QueryClient } from "@tanstack/react-query";

// single QueryClient instance for the entire app (!! do not change here every backend call depedns on this !!)
export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      refetchOnReconnect: false,
      retry: 1,
      staleTime: 1000 * 60 * 5, // global stale time of 5 minutes (for every get req)
      gcTime: 1000 * 60 * 10, // global cache time of 5 minutes (for every get req)
    },
    mutations: {
      retry: false,
    },
  },
});
