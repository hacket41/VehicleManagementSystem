import Footer from '#/components/Footer'
import Header from '#/components/Header'
import { Outlet, createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_main')({
  component: SiteLayout,
})

export default function SiteLayout() {
  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <main className="container mx-auto">
        <Outlet />
      </main>
      <Footer />
    </div>
  )
}
