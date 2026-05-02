import { queryOptions, useQuery } from '@tanstack/react-query'
import { createFileRoute, ErrorComponent } from '@tanstack/react-router'
import { Button } from '#/components/ui/button'
import { apiFetch } from '#/lib/api'
import { queryClient } from '#/lib/queryClient'

type MeResponse = {
  userId: string
  email: string
  role: string
  message: string
}

const getMe = () => {
  return queryOptions({
    queryKey: ['me'],
    queryFn: () => apiFetch<MeResponse>('/test/me'),
    staleTime: 1000 * 60 * 5,
    gcTime: 1000 * 60 * 5,
    refetchOnWindowFocus: false,
    refetchOnReconnect: true,
  })
}
const getAdminStatus = () => {
  return queryOptions({
    queryKey: ['admin'],
    queryFn: () => apiFetch<{ message: string }>('/test/admin-only'),
    staleTime: 1000 * 60 * 5,
    gcTime: 1000 * 60 * 5,
    refetchOnWindowFocus: false,
    refetchOnReconnect: true,
  })
}

export const Route = createFileRoute('/_main/')({
  // ssr: false,
  loader: () => queryClient.ensureQueryData(getMe()),
  errorComponent: ({ error }) => <ErrorComponent error={error} />,
  component: App,
})

function App() {
  // const me = Route.useLoaderData()
  const { data: me, isPending, isError } = useQuery(getMe())
  const adminQuery = useQuery({ ...getAdminStatus(), enabled: false })

  if (isPending) return <p>Loading...</p>
  if (!me) return <p>Loading...</p>
  if (isError) return <p>Error: {me.message}</p>

  return (
    <main className="container mx-auto p-8 space-y-6">
      <section className="border rounded p-4 space-y-2">
        <h2 className="font-bold text-lg">Token Valid</h2>
        <p>
          <span className="text-muted-foreground">User ID:</span> {me.userId}
        </p>
        <p>
          <span className="text-muted-foreground">Email:</span> {me.email}
        </p>
        <p>
          <span className="text-muted-foreground">Role:</span> {me.role}
        </p>
      </section>

      <section className="border rounded p-4 space-y-3">
        <h2 className="font-bold text-lg">Role Test</h2>
        <Button
          onClick={() => adminQuery.refetch()}
          disabled={adminQuery.isFetching}
        >
          Test
        </Button>
        {adminQuery.data && <p>{adminQuery.data.message}</p>}
      </section>
    </main>
  )
}
