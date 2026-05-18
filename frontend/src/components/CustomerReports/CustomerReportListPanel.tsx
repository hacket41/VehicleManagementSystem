import { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "#/components/ui/select";
import { ScrollArea } from "#/components/ui/scroll-area";
import { Skeleton } from "#/components/ui/skeleton";
import { Separator } from "#/components/ui/separator";
import {
  CustomerReportType,
  type CustomerReportDto,
} from "#/types/customerreport.types";
import { getCustomerReports } from "#/api/customerreport.api";
import { formatDate } from "#/utils/customerreport.utils";
import { cn } from "#/lib/utils";
import { FILTER_OPTIONS } from "./customerreport.constants";
import { ReportTypeBadge } from "./ReportTypeBadge";
import { ReportEmptyState } from "./ReportEmptyState";

interface CustomerReportListPanelProps {
  selectedId: number | null;
  onSelect: (report: CustomerReportDto) => void;
}

export function CustomerReportListPanel({
  selectedId,
  onSelect,
}: CustomerReportListPanelProps) {
  const [filter, setFilter] = useState<string>("all");

  const typeFilter =
    filter !== "all" ? (parseInt(filter) as CustomerReportType) : undefined;

  const {
    data: reports,
    isLoading,
    isError,
  } = useQuery(getCustomerReports(typeFilter));

  return (
    <div className="flex h-full flex-col gap-3">
      {/* Header + Filter */}
      <div className="flex items-center justify-between gap-2">
        <h2 className="text-xs font-semibold uppercase tracking-widest text-muted-foreground">
          Reports
        </h2>
        <Select value={filter} onValueChange={setFilter}>
          <SelectTrigger className="h-8 w-[160px] text-xs">
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            {FILTER_OPTIONS.map((opt) => (
              <SelectItem key={opt.value} value={opt.value} className="text-xs">
                {opt.label}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>

      <Separator />

      {/* List */}
      <ScrollArea className="flex-1">
        <div className="space-y-1 pr-2">
          {isLoading &&
            Array.from({ length: 5 }).map((_, i) => (
              <Skeleton key={i} className="h-16 w-full rounded-md" />
            ))}

          {isError && (
            <p className="py-8 text-center text-sm text-destructive">
              Failed to load reports.
            </p>
          )}

          {!isLoading && !isError && reports?.length === 0 && (
            <ReportEmptyState message="No reports found." />
          )}

          {reports?.map((report) => (
            <button
              key={report.id}
              onClick={() => onSelect(report)}
              className={cn(
                "w-full rounded-md border px-3 py-2.5 text-left transition-colors hover:bg-accent",
                selectedId === report.id
                  ? "border-primary bg-accent"
                  : "border-transparent bg-transparent",
              )}
            >
              <div className="flex items-center justify-between gap-2">
                <span className="text-sm font-medium">Report #{report.id}</span>
                <ReportTypeBadge type={report.type} />
              </div>
              <p className="mt-0.5 text-xs text-muted-foreground">
                {formatDate(report.generatedAt)} · {report.generatedBy}
              </p>
            </button>
          ))}
        </div>
      </ScrollArea>
    </div>
  );
}
