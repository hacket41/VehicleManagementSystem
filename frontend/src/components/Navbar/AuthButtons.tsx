import { Link } from '@tanstack/react-router'
import { useAuth } from '#/utils/AuthProvider'
import { Button } from '../ui/button'
import LogoutButton from './LogoutButton'

export default function AuthButtons() {
  // const { data: user } = useQuery(getMe()
  const { isAuthenticated, isPending } = useAuth()

  if (isPending) {
    return null
  }
  if (isAuthenticated) {
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
