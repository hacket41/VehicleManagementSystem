import { Button } from '#/components/ui/button'
import { apiFetch } from '#/lib/api'
import { createFileRoute, ErrorComponent, Link } from '@tanstack/react-router'
import { useState } from 'react'

type MeResponse = {
  userId: string
  email: string
  role: string
  message: string
}

export const Route = createFileRoute('/')({
  ssr: false,
  loader: () => apiFetch<MeResponse>('/test/me'),
  errorComponent: ({ error }) => <ErrorComponent error={error} />,
  component: App,
})

function App() {
  const me = Route.useLoaderData()
  const [adminResult, setAdminResult] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)

  async function testAdminEndpoint() {
    setLoading(true)
    try {
      const res = await apiFetch<{ message: string }>('/test/admin-only')
      setAdminResult(` ${res.message}`)
    } catch (e) {
      setAdminResult(`${(e as Error).message}`)
    } finally {
      setLoading(false)
    }
  }

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
        <Button onClick={testAdminEndpoint} disabled={loading}>
          {loading ? 'Testing...' : 'Hit Admin-Only Endpoint'}
        </Button>
        {adminResult && <p>{adminResult}</p>}
      </section>
    </main>
  )
}
