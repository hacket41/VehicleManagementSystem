import { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import { format } from "date-fns";
import { CalendarDays, ChevronRight, Search } from "lucide-react";

import { Input } from "#/components/ui/input";
import { ScrollArea } from "#/components/ui/scroll-area";
import { Skeleton } from "#/components/ui/skeleton";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "#/components/ui/select";
import { cn } from "#/lib/utils";

import { getCustomerReports } from "#/api/customerreport.api";
import {
  CustomerReportType,
  type CustomerReportDto,
} from "#/types/customerreport.types";
import { ReportTypeBadge } from "./ReportTypeBadge";
import { GenerateReportDialog } from "./GenerateReportDialog";
import {
  ALL_REPORT_TYPES,
  REPORT_TYPE_LABELS,
} from "./customerreport.constants";

interface CustomerReportListPanelProps {
  selectedId: number | null;
  onSelect: (report: CustomerReportDto) => void;
}

export function CustomerReportListPanel({
  selectedId,
  onSelect,
}: CustomerReportListPanelProps) {
  const [typeFilter, setTypeFilter] = useState<CustomerReportType | "all">(
    "all",
  );
  const [search, setSearch] = useState("");

  const { data: reports, isLoading } = useQuery(
    getCustomerReports(typeFilter === "all" ? undefined : typeFilter),
  );

  const filtered = (reports ?? []).filter((r) => {
    if (!search) return true;
    const q = search.toLowerCase();
    return (
      r.generatedBy.toLowerCase().includes(q) ||
      r.type.toLowerCase().includes(q) ||
      r.id.toString().includes(q)
    );
  });

  return (
    <div className="flex flex-col h-full border-r bg-background">
      {/* Header */}
      <div className="p-4 border-b space-y-3">
        <div className="flex items-center justify-between">
          <h2 className="text-sm font-semibold tracking-tight">
            Customer Reports
          </h2>
          <GenerateReportDialog />
        </div>

        {/* Search */}
        <div className="relative">
          <Search className="absolute left-2.5 top-2.5 h-3.5 w-3.5 text-muted-foreground" />
          <Input
            placeholder="Search reports…"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            className="pl-8 h-8 text-sm"
          />
        </div>

        {/* Type filter */}
        <Select
          value={typeFilter === "all" ? "all" : String(typeFilter)}
          onValueChange={(v) =>
            setTypeFilter(
              v === "all" ? "all" : (Number(v) as CustomerReportType),
            )
          }
        >
          <SelectTrigger className="h-8 text-xs">
            <SelectValue placeholder="All types" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="all">All Types</SelectItem>
            {ALL_REPORT_TYPES.map((t) => (
              <SelectItem key={t} value={String(t)}>
                {REPORT_TYPE_LABELS[t]}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>

      {/* List */}
      <ScrollArea className="flex-1">
        {isLoading ? (
          <div className="p-3 space-y-2">
            {Array.from({ length: 5 }).map((_, i) => (
              <Skeleton key={i} className="h-16 w-full rounded-md" />
            ))}
          </div>
        ) : filtered.length === 0 ? (
          <div className="p-6 text-center text-xs text-muted-foreground">
            No reports found.
          </div>
        ) : (
          <div className="p-2 space-y-1">
            {filtered.map((report) => (
              <ReportListItem
                key={report.id}
                report={report}
                isSelected={selectedId === report.id}
                onClick={() => onSelect(report)}
              />
            ))}
          </div>
        )}
      </ScrollArea>
    </div>
  );
}

interface ReportListItemProps {
  report: CustomerReportDto;
  isSelected: boolean;
  onClick: () => void;
}

function ReportListItem({ report, isSelected, onClick }: ReportListItemProps) {
  return (
    <button
      onClick={onClick}
      className={cn(
        "w-full text-left rounded-md px-3 py-2.5 transition-colors group",
        "hover:bg-accent hover:text-accent-foreground",
        isSelected && "bg-accent text-accent-foreground",
      )}
    >
      <div className="flex items-start justify-between gap-2">
        <div className="min-w-0 flex-1 space-y-1">
          <div className="flex items-center gap-1.5 flex-wrap">
            <ReportTypeBadge type={report.type} />
            <span className="text-xs text-muted-foreground">#{report.id}</span>
          </div>
          <div className="flex items-center gap-1 text-xs text-muted-foreground">
            <CalendarDays className="h-3 w-3 shrink-0" />
            <span>
              {format(new Date(report.generatedAt), "MMM d, yyyy · h:mm a")}
            </span>
          </div>
          <p className="text-xs truncate text-muted-foreground">
            By {report.generatedBy} · {report.rows.length} rows
          </p>
        </div>
        <ChevronRight
          className={cn(
            "h-3.5 w-3.5 shrink-0 mt-1 text-muted-foreground transition-transform",
            isSelected && "text-foreground",
          )}
        />
      </div>
    </button>
  );
}
