import { createFileRoute, Outlet } from '@tanstack/react-router'
import Footer from '#/components/Footer'
import Header from '#/components/Header'

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
