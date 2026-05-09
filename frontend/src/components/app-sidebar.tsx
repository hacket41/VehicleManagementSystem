import {
  Bolt,
  CarFrontIcon,
  CommandIcon,
  FileChartColumnIcon,
  Home,
  Mail,
  Settings2Icon,
  UserStarIcon,
} from 'lucide-react'
import type * as React from 'react'
import { NavDocuments } from '#/components/nav-documents'
import { NavMain } from '#/components/nav-main'
import { NavSecondary } from '#/components/nav-secondary'
import { NavUser } from '#/components/nav-user'
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuItem,
} from '#/components/ui/sidebar'

const data = {
  user: {
    name: 'shadcn',
    email: 'm@example.com',
    avatar: '/avatars/shadcn.jpg',
  },
  navMain: [
    {
      title: 'Home',
      url: '/dashboard',
      icon: <Home />,
    },
    {
      title: 'Parts Management',
      url: '/dashboard/parts',
      icon: <Bolt />,
    },
    {
      title: 'Vehicle Management',
      url: '/dashboard/vehicles',
      icon: <CarFrontIcon />,
    },
    {
      title: 'Vendor Management',
      url: '/dashboard/vendors',
      icon: <UserStarIcon />,
    },
  ],

  navSecondary: [
    {
      title: 'Settings',
      url: '#',
      icon: <Settings2Icon />,
    },
  ],
  documents: [
    {
      name: 'Notifications',
      url: '/dashboard/notifications',
      icon: <Mail />,
    },
    {
      name: 'Reports',
      url: '#',
      icon: <FileChartColumnIcon />,
    },
  ],
}
export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  return (
    <Sidebar collapsible="offcanvas" {...props}>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <div className="flex gap-4 p-2">
              <CommandIcon className="size-5!" />
              <span className="text-base font-semibold">Acme Inc.</span>
            </div>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavDocuments items={data.documents} />
        <NavSecondary items={data.navSecondary} className="mt-auto" />
      </SidebarContent>
      <SidebarFooter>
        <NavUser />
      </SidebarFooter>
    </Sidebar>
  )
}
