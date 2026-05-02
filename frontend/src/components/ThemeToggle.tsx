import { useEffect, useState } from 'react'
import { Switch } from './ui/switch'
import {
  CloudMoonRainIcon,
  MoonStar,
  SunIcon,
  type CloudMoonIcon,
} from 'lucide-react'
import { Label } from './ui/label'
import { FieldLabel } from './ui/field'

type ThemeMode = 'light' | 'dark'

function getInitialMode(): ThemeMode {
  if (typeof window === 'undefined') {
    return 'light'
  }

  const stored = window.localStorage.getItem('theme')
  return stored === 'dark' ? 'dark' : 'light'
}

function applyThemeMode(mode: ThemeMode) {
  document.documentElement.classList.remove('light', 'dark')
  document.documentElement.classList.add(mode)

  document.documentElement.setAttribute('data-theme', mode)
  document.documentElement.style.colorScheme = mode
}

export default function ThemeToggle() {
  const [mode, setMode] = useState<ThemeMode>('light')

  useEffect(() => {
    const initialMode = getInitialMode()
    setMode(initialMode)
    applyThemeMode(initialMode)
  }, [])

  function toggleMode() {
    const nextMode: ThemeMode = mode === 'light' ? 'dark' : 'light'
    setMode(nextMode)
    applyThemeMode(nextMode)
    window.localStorage.setItem('theme', nextMode)
  }

  return (
    <FieldLabel>
      <Label htmlFor="theme-toggle">
        {mode === 'dark' ? (
          <MoonStar className="text-primary" size={'20'} />
        ) : (
          <SunIcon className="text-primary" size={'20'} />
        )}{' '}
        <Switch
          role="switch"
          id="theme-toggle"
          checked={mode === 'dark'}
          onCheckedChange={toggleMode}
        />
      </Label>
    </FieldLabel>
  )
}
