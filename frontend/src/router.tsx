import { QueryClient } from '@tanstack/react-query'
import { createRouter as createTanStackRouter } from '@tanstack/react-router'
import { setupRouterSsrQueryIntegration } from '@tanstack/react-router-ssr-query'
import { routeTree } from './routeTree.gen'
import type { AuthState } from './utils/AuthProvider'

export function getRouter() {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        refetchOnWindowFocus: false,
        refetchOnReconnect: false,
        retry: 1,
        staleTime: 1000 * 60 * 5,
        gcTime: 1000 * 60 * 10,
      },
      mutations: {
        retry: false,
      },
    },
  })
  const router = createTanStackRouter({
    routeTree,
    context: {
      queryClient,
      authState: undefined as unknown as AuthState,
    },
    scrollRestoration: true,
    defaultPreload: 'intent',
    defaultPreloadStaleTime: 0,
  })
  setupRouterSsrQueryIntegration({
    router,
    queryClient,
    dehydrateOptions: {
      shouldDehydrateMutation: () => false, // exclude mutations
      shouldDehydrateQuery: () => true,
    },
    // optional:
    // handleRedirects: true,
    // wrapQueryClient: true,
  })

  return router
}

declare module '@tanstack/react-router' {
  interface Register {
    router: ReturnType<typeof getRouter>
  }
}
