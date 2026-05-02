;(() => {
  try {
    const stored = window.localStorage.getItem('theme')
    const mode = stored === 'dark' ? 'dark' : 'light'
    const root = document.documentElement

    root.classList.remove('light', 'dark')
    root.classList.add(mode)
    root.setAttribute('data-theme', mode)
    root.style.colorScheme = mode
  } catch (e) {
    console.error(e)
  }
})()
