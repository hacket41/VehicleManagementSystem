import { Link } from '@tanstack/react-router'
import { useAuth } from '#/hooks/useAuth'
import { Button } from '../ui/button'
import LogoutButton from './LogoutButton'

export default function AuthButtons() {
  const { user, isPending, isError } = useAuth()
  console.log(user)
  if (isPending) return null
  return (
    <>
      {user == null ? (
        <div className="flex gap-2">
          <Link to="/login">
            <Button type="button" className={'primary'}>
              Login
            </Button>
          </Link>
          <Link to="/signup">
            <Button type="button" variant={'secondary'}>
              Signup
            </Button>
          </Link>
        </div>
      ) : (
        <LogoutButton />
      )}
    </>
  )
}
