import { queryOptions, useQuery } from '@tanstack/react-query'
import { createFileRoute, ErrorComponent } from '@tanstack/react-router'
import { getMe } from '#/api/auth.api'
import { Button } from '#/components/ui/button'
import { apiFetch } from '#/lib/api'
import { queryClient } from '#/lib/queryClient'

export type MeResponse = {
  id: string
  name: string
  email: string
  phoneNumber: string
  roles: string[]
}

const getAdminStatus = () => {
  return queryOptions({
    queryKey: ['admin'],
    queryFn: () => apiFetch<{ message: string }>('/test/admin-only'),
  })
}

export const Route = createFileRoute('/_main/')({
  loader: () => queryClient.ensureQueryData(getMe()),
  errorComponent: ({ error }) => <ErrorComponent error={error} />,
  component: App,
})

function App() {
  const { data: me, isPending, isError } = useQuery(getMe())
  const adminQuery = useQuery({ ...getAdminStatus(), enabled: false })

  if (isPending) return <p>Loading...</p>
  if (!me) return <p>Loading...</p>
  if (isError) return <p>Error</p>

  return (
    <main className="container mx-auto p-8 space-y-6">
      <section className="border rounded p-4 space-y-2">
        <h2 className="font-bold text-lg">Token Valid</h2>
        <p>
          <span className="text-muted-foreground">User ID:</span> {me.id}
        </p>
        <p>
          <span className="text-muted-foreground">Email:</span> {me.email}
        </p>
        <p>
          <span className="text-muted-foreground">Role:</span>{' '}
          {`${me.roles[0]} , ${me.roles[1]}`}
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
