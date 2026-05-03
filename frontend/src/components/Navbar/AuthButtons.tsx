import { Link } from '@tanstack/react-router'
import { useAuth } from '#/providers/AuthContextProvider'
import { Button } from '../ui/button'
import LogoutButton from './LogoutButton'

export default function AuthButtons() {
  const { user } = useAuth()

  if (user) {
    return <LogoutButton />
  }
  return (
    <div className="flex gap-2">
      <Link to="/login">
        <Button type="button">Login</Button>
      </Link>
      <Link to="/signup">
        <Button type="button" variant="secondary">
          Signup
        </Button>
      </Link>
    </div>
  )
}
