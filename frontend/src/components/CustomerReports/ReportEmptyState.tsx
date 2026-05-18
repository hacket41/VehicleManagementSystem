import { FileText } from "lucide-react";

interface ReportEmptyStateProps {
  message: string;
}

export function ReportEmptyState({ message }: ReportEmptyStateProps) {
  return (
    <div className="flex flex-col items-center gap-2 py-12 text-muted-foreground">
      <FileText className="h-10 w-10 opacity-30" />
      <p className="text-sm">{message}</p>
    </div>
  );
}
