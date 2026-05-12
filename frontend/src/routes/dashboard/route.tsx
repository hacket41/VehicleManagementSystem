import { createFileRoute, Outlet, redirect } from '@tanstack/react-router'
import { AppSidebar } from '#/components/app-sidebar'
import { SiteHeader } from '#/components/site-header'
import { SidebarInset, SidebarProvider } from '#/components/ui/sidebar'

export const Route = createFileRoute('/dashboard')({
  component: RouteComponent,
  // loader: async ({ context }) => {
  //   const { user } = context
  //   const hasAccess =
  //     user?.roles.includes('Admin') || user?.roles.includes('Staff')

  //   if (!hasAccess) {
  //     throw redirect({ to: '/' })
  //   }
  // },
})

function RouteComponent() {
  return (
    <SidebarProvider
      style={
        {
          '--sidebar-width': 'calc(var(--spacing) * 72)',
          '--header-height': 'calc(var(--spacing) * 12)',
        } as React.CSSProperties
      }
    >
      <AppSidebar variant="floating" className="p-4" />
      <SidebarInset>
        <SiteHeader />
        <div className="container mx-auto py-4">
          <Outlet />
        </div>
      </SidebarInset>
    </SidebarProvider>
  )
}
