import { Link } from '@tanstack/react-router'
import ThemeToggle from './ThemeToggle'

export default function Header() {
  return (
    <header className="container mx-auto sticky top-0 z-50 border-b backdrop-blur-lg px-4 py-6 flex justify-between items-center">
      <div>
        <Link to="/">
          <h1 className="text-2xl font-bold">My App</h1>
        </Link>

        <Link to="/dashboard">Dashboard</Link>
      </div>
      <ThemeToggle />
    </header>
  )
}
