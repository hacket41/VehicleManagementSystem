import { Badge } from "#/components/ui/badge";
import { CustomerReportType } from "#/types/customerreport.types";
import { REPORT_TYPE_META } from "./customerreport.constants";

interface ReportTypeBadgeProps {
  type: CustomerReportType;
}

export function ReportTypeBadge({ type }: ReportTypeBadgeProps) {
  const meta = REPORT_TYPE_META[type];
  if (!meta) return null;
  return (
    <Badge variant={meta.variant} className="gap-1 text-xs">
      <meta.icon className="h-3 w-3" />
      {meta.label}
    </Badge>
  );
}
