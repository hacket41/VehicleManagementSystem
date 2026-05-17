import { Loader2Icon } from 'lucide-react'
import { cn } from '#/lib/utils'

function Spinner({ className, ...props }: React.ComponentProps<'svg'>) {
  return (
    <Loader2Icon
      role="status"
      aria-label="Loading"
      className={cn('size-4 animate-spin', className)}
      {...props}
    />
  )
}

export { Spinner }

function CenteredSpinner({ className, ...props }: React.ComponentProps<'div'>) {
  return (
    <div
      className={cn(
        'container mx-auto flex items-center justify-center min-h-[60vh]',
        className,
      )}
      {...props}
    >
      <Spinner className="size-16" />
    </div>
  )
}

export { CenteredSpinner }
