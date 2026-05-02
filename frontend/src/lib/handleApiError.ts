import { toast } from 'sonner'
import { ApiError } from './api'

export function handleApiError(err: unknown) {
  if (!(err instanceof ApiError)) {
    toast.error('Something went wrong')
    return
  }

  if (err.validationErrors) {
    for (const messages of Object.values(err.validationErrors)) {
      for (const message of messages) {
        toast.error(message)
      }
    }
    return
  }

  toast.error(err.detail ?? err.message)
}
