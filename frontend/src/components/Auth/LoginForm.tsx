import { useMutation } from '@tanstack/react-query'
import { useRouter } from '@tanstack/react-router'
import { Eye, EyeOff } from 'lucide-react'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { toast } from 'sonner'
import { login } from '#/api/auth.api'
import { queryClient } from '#/lib/queryClient'
import { Button } from '@/components/ui/button'
import {
  Field,
  FieldDescription,
  FieldGroup,
  FieldLabel,
} from '@/components/ui/field'
import { Input } from '@/components/ui/input'

export type LoginPayload = {
  email: string
  password: string
}

export function LoginForm() {
  const [showPassword, setShowPassword] = useState(false)
  const router = useRouter()
  const form = useForm<LoginPayload>({
    defaultValues: {
      email: '',
      password: '',
    },
  })
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = form

  const { mutate } = useMutation({
    mutationFn: login,
    onSuccess: () => {
      toast.success('Logged in successfully')
      queryClient.refetchQueries({ queryKey: ['me'] })
      router.navigate({ to: '/' })
    },
    onError: (e) => {
      toast.error(e instanceof Error ? e.message : 'Invalid credentials')
    },
  })

  const onSubmit = (data: LoginPayload) => {
    console.log(data)
    mutate(data)
  }

  return (
    <form className="w-full max-w-sm" onSubmit={handleSubmit(onSubmit)}>
      <FieldGroup>
        <Field>
          <FieldLabel htmlFor="email">Email</FieldLabel>
          <Input
            id="email"
            type="email"
            placeholder="you@example.com"
            {...register('email', { required: 'Email is required' })}
          />
          {errors.email && (
            <span className="text-xs">{errors.email.message}</span>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="password" className="flex justify-between">
            Password
          </FieldLabel>

          <div className="relative">
            <Input
              className="pr-10"
              id="password"
              type={showPassword ? 'text' : 'password'}
              placeholder="********"
              {...register('password', { required: 'Password is required' })}
            />
            <Button
              variant={'link'}
              size={'icon'}
              onClick={() => setShowPassword(!showPassword)}
              className="absolute right-2"
            >
              {showPassword ? <EyeOff /> : <Eye />}
            </Button>
          </div>
          {errors.password && (
            <span className="text-xs">{errors.password.message}</span>
          )}
          <FieldDescription>
            We&apos;ll never share your password with anyone.
          </FieldDescription>
        </Field>

        <Field orientation="horizontal">
          <Button type="button" variant="outline">
            Cancel
          </Button>
          <Button type="submit">Submit</Button>
        </Field>
      </FieldGroup>
    </form>
  )
}
