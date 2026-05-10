import { Link } from '@tanstack/react-router'
import { HomeIcon } from 'lucide-react'
import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyTitle,
} from '@/components/ui/empty'
import { Button } from './ui/button'

export default function NotFound() {
  return (
    <main className="flex min-h-screen items-center justify-center bg-background px-6">
      <div className="w-full max-w-2xl">
        <Empty className="rounded-3xl border bg-card/50 px-10 py-16 shadow-xl backdrop-blur">
          <EmptyHeader className="space-y-6 text-center">
            <div className="text-8xl font-black tracking-tight text-primary">
              404
            </div>

            <div className="space-y-3">
              <EmptyTitle className="text-4xl font-bold">
                Page Not Found
              </EmptyTitle>

              <EmptyDescription className="mx-auto max-w-md text-base text-muted-foreground">
                The page you&apos;re trying to access doesn&apos;t exist or may
                have been moved somewhere else.
              </EmptyDescription>
            </div>
          </EmptyHeader>

          <EmptyContent className="mt-10 flex justify-center">
            <Link to="/">
              <Button size="lg" className="h-12 rounded-xl px-8 text-base">
                <HomeIcon className="size-5" />
                Go Back Home
              </Button>
            </Link>
          </EmptyContent>
        </Empty>
      </div>
    </main>
  )
}
