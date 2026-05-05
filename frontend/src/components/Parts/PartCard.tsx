import { HeartIcon } from 'lucide-react'
import { useState } from 'react'
import { cn } from '#/lib/utils'
import type { Part } from '#/types/parts.types'
import { Badge } from '../ui/badge'
import { Button } from '../ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from '../ui/card'

export default function PartCard({ part }: { part: Part }) {
  const [liked, setLiked] = useState<boolean>(false)

  return (
    <div className="relative max-w-sm  ">
      <div className="flex h-60 items-center justify-center">
        <img src="/spark-plug.jpg" alt="Shoes" className="w-75" />
      </div>
      <Button
        size="icon"
        onClick={() => setLiked(!liked)}
        className="bg-primary/10 hover:bg-primary/20 absolute top-4 right-4 rounded-full"
      >
        <HeartIcon
          className={cn(
            liked ? 'fill-destructive stroke-destructive' : 'stroke-white',
          )}
        />
        <span className="sr-only">Like</span>
      </Button>
      <Card className="border-none">
        <CardHeader>
          <CardTitle>{part.name}</CardTitle>
          <CardDescription className="flex items-center gap-2 py-1">
            <Badge variant="outline" className="rounded-sm">
              {part.partNumber}
            </Badge>
          </CardDescription>
        </CardHeader>
        <CardContent>
          <p>{part.description}</p>
        </CardContent>
        <CardFooter className="justify-between gap-3 max-sm:flex-col max-sm:items-stretch">
          <div className="flex flex-col">
            <span className="text-sm font-medium uppercase">Price</span>
            <span className="text-xl font-semibold">
              <span className="text-sm mr-2 font-semibold text-primary/80">
                Nrs
              </span>
              {part.sellingPrice}
            </span>
          </div>
          <Button size="lg">Add to cart</Button>
        </CardFooter>
      </Card>
    </div>
  )
}
