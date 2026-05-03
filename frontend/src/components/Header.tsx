import { Link } from '@tanstack/react-router'
import AuthButtons from './Navbar/AuthButtons'
import ThemeToggle from './ThemeToggle'

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
      <div className="flex gap-8">
        <AuthButtons />
        <ThemeToggle />
      </div>
    </nav>
  )
}
