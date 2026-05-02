import { Link } from '@tanstack/react-router'
import LogoutButton from './Navbar/LogoutButton'
import ThemeToggle from './ThemeToggle'
import { Button } from './ui/button'

export default function Header() {
  return (
    <nav className="container mx-auto sticky top-0 z-50 border-b backdrop-blur-lg px-4 py-6 flex justify-between items-center">
      <div className="flex justify-between">
        <Link to="/">
          <h1 className="text-2xl font-bold">Nep-Auto</h1>
        </Link>
      </div>

      <div className="flex gap-6">
        <Link to="/dashboard">Dashboard</Link>
        <Link to="/about">About</Link>
        <Link to="/test">Test</Link>
      </div>

      <div className="flex gap-16">
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
            <LogoutButton />
          </Link>
        </div>
        <ThemeToggle />
      </div>
    </nav>
  )
}
