import { apiFetch } from '#/lib/api'
import { createFileRoute, ErrorComponent } from '@tanstack/react-router'

type TestEndpoint = {
  message: string
}
export const Route = createFileRoute('/')({
  loader: () => apiFetch<TestEndpoint>('/Test'),
  errorComponent: ({ error }) => <ErrorComponent error={error} />,
  component: App,
})

function App() {
  const TestResponse = Route.useLoaderData()
  return (
    <main className="page-wrap px-4 pb-8 pt-14">
      <h1>{TestResponse.message}</h1>
    </main>
  )
}
