import { useMutation } from '@tanstack/react-query'
import { useRouter } from '@tanstack/react-router'
import { useForm } from 'react-hook-form'
import { toast } from 'sonner'
import { registerCustomer } from '#/api/auth.api'
import { handleFormApiError } from '#/lib/handleApiError'
import { queryClient } from '#/lib/queryClient'
import { Button } from '@/components/ui/button'
import {
  Field,
  FieldDescription,
  FieldGroup,
  FieldLabel,
} from '@/components/ui/field'
import { Input } from '@/components/ui/input'
import { Spinner } from '../ui/spinner'

export type RegisterPayload = {
  firstName: string
  lastName: string
  email: string
  password: string
  phone: string
  address: string
}

export function SignupForm() {
  const router = useRouter()
  const form = useForm<RegisterPayload>({
    defaultValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      phone: '',
      address: '',
    },
  })

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = form

  const { mutate, isPending } = useMutation({
    mutationFn: registerCustomer,
    onSuccess: () => {
      router.navigate({ to: '/' })
      queryClient.invalidateQueries({ queryKey: ['me'] })
      toast.success('Account created successfully')
    },
    onError: (e) => handleFormApiError(e, form),
  })

  function onSubmit(data: RegisterPayload) {
    console.log(data)
    mutate(data)
  }

  return (
    <form className="w-full max-w-sm" onSubmit={handleSubmit(onSubmit)}>
      <FieldGroup>
        <FieldGroup className="flex flex-row">
          <Field>
            <FieldLabel htmlFor="firstName">First Name</FieldLabel>
            <Input
              id="firstName"
              type="text"
              placeholder="John"
              {...register('firstName', { required: 'First name is required' })}
            />
            {errors.firstName && (
              <span className="text-primary text-xs">
                {errors.firstName.message}
              </span>
            )}
          </Field>
          <Field>
            <FieldLabel htmlFor="lastName">Last Name</FieldLabel>
            <Input
              id="lastName"
              type="text"
              placeholder="Doe"
              {...register('lastName', { required: 'Last name is required' })}
            />
            {errors.lastName && (
              <span className="text-primary text-xs ">
                {errors.lastName.message}
              </span>
            )}
          </Field>
        </FieldGroup>
        <Field>
          <FieldLabel htmlFor="email">Email</FieldLabel>
          <Input
            id="email"
            type="email"
            placeholder="john@example.com"
            {...register('email', { required: 'Email is required' })}
          />
          {errors.email && (
            <span className="text-primary text-xs">{errors.email.message}</span>
          )}
          <FieldDescription>
            We&apos;ll never share your email with anyone.
          </FieldDescription>
        </Field>
        <Field>
          <FieldLabel htmlFor="password">Password</FieldLabel>
          <Input
            id="password"
            type="password"
            placeholder="********"
            {...register('password', { required: 'Password is required' })}
          />
          {errors.password && (
            <span className="text-primary text-xs">
              {errors.password.message}
            </span>
          )}
          <FieldDescription>
            Please enter a strong password for your account.
          </FieldDescription>
        </Field>
        <div>
          <Field>
            <FieldLabel htmlFor="phone">Phone</FieldLabel>
            <Input
              id="phone"
              type="text"
              placeholder="+1 (555) 123-4567"
              {...register('phone')}
            />
            {errors.phone && (
              <span className="text-primary text-xs">
                {errors.phone.message}
              </span>
            )}
          </Field>
        </div>
        <Field>
          <FieldLabel htmlFor="address">Address</FieldLabel>
          <Input
            id="address"
            type="text"
            placeholder="123 Main St"
            {...register('address', { required: 'Address is required' })}
          />
          {errors.address && (
            <span className="text-primary text-xs">
              {errors.address.message}
            </span>
          )}
        </Field>
        <Field orientation="horizontal">
          <Button type="button" variant="outline">
            Cancel
          </Button>
          <Button type="submit">{isPending ? <Spinner /> : 'Submit'}</Button>
        </Field>
      </FieldGroup>
    </form>
  )
}
