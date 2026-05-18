import { FileBarChart } from "lucide-react";

interface ReportEmptyStateProps {
  title?: string;
  description?: string;
}

export function ReportEmptyState({
  title = "No report selected",
  description = "Select a report from the list or generate a new one to view details.",
}: ReportEmptyStateProps) {
  return (
    <div className="flex flex-col items-center justify-center h-full min-h-[300px] gap-4 text-center p-8">
      <div className="rounded-full bg-muted p-4">
        <FileBarChart className="h-8 w-8 text-muted-foreground" />
      </div>
      <div className="space-y-1">
        <p className="text-sm font-medium text-foreground">{title}</p>
        <p className="text-xs text-muted-foreground max-w-xs">{description}</p>
      </div>
    </div>
  );
}
