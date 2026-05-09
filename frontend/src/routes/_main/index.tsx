import { createFileRoute } from '@tanstack/react-router'



export const Route = createFileRoute('/_main/')({
  component: App,
})

function App() {
  // const { data: me } = useQuery(getMe())

  return (
    <main className="container mx-auto p-8 space-y-6">
      <section className="border rounded p-4 space-y-3"></section>
    </main>
  )
}
