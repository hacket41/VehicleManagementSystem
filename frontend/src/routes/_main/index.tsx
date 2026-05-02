import { useQuery } from '@tanstack/react-query'
import { createFileRoute } from '@tanstack/react-router'
import { getMe } from '#/api/auth.api'

export type MeResponse = {
  id: string
  name: string
  email: string
  phoneNumber: string
  roles: string[]
}

export const Route = createFileRoute('/_main/')({
  component: App,
})

function App() {
  // const { data: me } = useQuery(getMe())

  return (
    <main className="container mx-auto p-8 space-y-6">
      <section className="border rounded p-4 space-y-3">
        {/*<h3>{!me ? 'You are not logged in' : 'You are logged in'}</h3>*/}
      </section>
    </main>
  )
}
