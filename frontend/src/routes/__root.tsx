import { TanStackDevtools } from '@tanstack/react-devtools'
import { type QueryClient, QueryClientProvider } from '@tanstack/react-query'
import {
  createRootRouteWithContext,
  HeadContent,
  Outlet,
  Scripts,
} from '@tanstack/react-router'
import { TanStackRouterDevtoolsPanel } from '@tanstack/react-router-devtools'
import { Toaster } from '#/components/ui/sonner'
import { queryClient } from '#/lib/queryClient'
import { AuthProvider } from '#/providers/AuthContextProvider'
import appCss from '../styles.css?url'

interface MyRouterContext {
  queryClient: QueryClient
}
export const Route = createRootRouteWithContext<MyRouterContext>()({
  head: () => ({
    meta: [
      {
        charSet: 'utf-8',
      },
      {
        name: 'viewport',
        content: 'width=device-width, initial-scale=1',
      },
      {
        title: 'Nep-Auto',
      },
    ],
    links: [
      {
        rel: 'stylesheet',
        href: appCss,
      },
      {
        rel: 'preload',
        href: '/theme-init.js',
        as: 'script',
      },
    ],
  }),
  shellComponent: RootDocument,
})

function RootDocument() {
  return (
    <html lang="en" suppressHydrationWarning>
      <head>
        <script src="/theme-init.js" />
        <HeadContent />
      </head>

      <body className="font-sans antialiased">
        <QueryClientProvider client={queryClient}>
          <AuthProvider>
            <Outlet />
            <Toaster position="top-center" richColors closeButton />
          </AuthProvider>
        </QueryClientProvider>

        <TanStackDevtools
          config={{
            position: 'bottom-right',
          }}
          plugins={[
            {
              name: 'Tanstack Router',
              render: <TanStackRouterDevtoolsPanel />,
            },
          ]}
        />
        <Scripts />
      </body>
    </html>
  )
}
