import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_main/')({
  component: App,
})

function App() {
  return (
    <main className="container mx-auto  p-8 space-y-6">
      {/*<Button onClick={() => mutate()}>Refresh Token</Button>*/}
      {/*<UploadButton
        endpoint={'imageUploader'}
        className="ut-button:bg-primary ut-button:text-primary-foreground hover:ut-button:bg-primary/80 ut-button:h-6 ut-button:gap-1 ut-button:px-2 ut-button:text-xs"
      />
      <UploadDropzone
        endpoint={'imageUploader'}
        className="ut-dropzone:bg-secondary ut-dropzone:text-secondary-foreground hover:ut-dropzone:bg-secondary/80 ut-dropzone:h-6 ut-dropzone:gap-1 ut-dropzone:px-2 ut-dropzone:text-xs"
      />*/}
    </main>
  )
}
