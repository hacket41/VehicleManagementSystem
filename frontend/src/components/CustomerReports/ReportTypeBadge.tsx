import { Badge } from "#/components/ui/badge";
import { CustomerReportType } from "#/types/customerreport.types";
import {
  REPORT_TYPE_COLORS,
  REPORT_TYPE_LABELS,
} from "./customerreport.constants";

interface ReportTypeBadgeProps {
  type: CustomerReportType | string;
  className?: string;
}

function resolveType(
  type: CustomerReportType | string,
): CustomerReportType | null {
  if (typeof type === "number") return type as CustomerReportType;
  const entry = Object.entries(CustomerReportType).find(
    ([key]) => key.toLowerCase() === String(type).toLowerCase(),
  );
  if (entry) return entry[1] as CustomerReportType;
  return null;
}

export function ReportTypeBadge({ type, className }: ReportTypeBadgeProps) {
  const resolved = resolveType(type);

  if (resolved === null) {
    return (
      <Badge variant="outline" className={className}>
        {String(type)}
      </Badge>
    );
  }

  const colors = REPORT_TYPE_COLORS[resolved];

  return (
    <Badge
      variant="outline"
      className={`border font-medium ${colors.badge} ${className ?? ""}`}
    >
      {REPORT_TYPE_LABELS[resolved]}
    </Badge>
  );
}
