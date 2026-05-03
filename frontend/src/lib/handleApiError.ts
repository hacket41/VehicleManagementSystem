import type { FieldValues, Path, UseFormReturn } from 'react-hook-form'
import { toast } from 'sonner'
import { ApiError } from '#/lib/api'

export function handleFormApiError<T extends FieldValues>(
  error: unknown,
  form: UseFormReturn<T>,
) {
  if (!(error instanceof ApiError)) {
    toast.error('Something went wrong')
    return
  }

  if (!error.errors) {
    toast.error(error.message)
    return
  }

  let hasFieldError = false

  Object.entries(error.errors).forEach(([field, messages]) => {
    const key = field.toLowerCase() as Path<T>

    if (key in form.getValues()) {
      hasFieldError = true

      form.setError(key, {
        type: 'server',
        message: messages[0],
      })
    }
  })

  if (!hasFieldError) {
    toast.error(error.message)
  }
}
