import { useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { CalendarIcon, Loader2, Plus } from "lucide-react";

import { Button } from "#/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "#/components/ui/dialog";
import { Label } from "#/components/ui/label";
import { Input } from "#/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "#/components/ui/select";

import { generateCustomerReport } from "#/api/customerreport.api";
import {
  CustomerReportType,
  type GenerateCustomerReportRequest,
} from "#/types/customerreport.types";
import {
  ALL_REPORT_TYPES,
  REPORT_TYPE_DESCRIPTIONS,
  REPORT_TYPE_LABELS,
} from "./customerreport.constants";

const PERIOD_REQUIRED_TYPES = [
  CustomerReportType.TopSpenders,
  CustomerReportType.RegularCustomers,
];

export function GenerateReportDialog() {
  const queryClient = useQueryClient();
  const [open, setOpen] = useState(false);
  const [reportType, setReportType] = useState<CustomerReportType | "">("");
  const [periodStart, setPeriodStart] = useState("");
  const [periodEnd, setPeriodEnd] = useState("");

  const needsPeriod =
    reportType !== "" &&
    PERIOD_REQUIRED_TYPES.includes(reportType as CustomerReportType);

  const mutation = useMutation({
    mutationFn: (payload: GenerateCustomerReportRequest) =>
      generateCustomerReport(payload),
    onSuccess: () => {
      toast.success("Report generated successfully.");
      queryClient.invalidateQueries({ queryKey: ["customer-reports"] });
      setOpen(false);
      resetForm();
    },
    onError: () => {
      toast.error("Failed to generate report. Please try again.");
    },
  });

  function resetForm() {
    setReportType("");
    setPeriodStart("");
    setPeriodEnd("");
  }

  function handleSubmit() {
    if (reportType === "") return;

    const payload: GenerateCustomerReportRequest = {
      type: reportType as CustomerReportType,
      ...(needsPeriod && periodStart ? { periodStart } : {}),
      ...(needsPeriod && periodEnd ? { periodEnd } : {}),
    };

    mutation.mutate(payload);
  }

  const isValid =
    reportType !== "" &&
    (!needsPeriod || (periodStart !== "" && periodEnd !== ""));

  return (
    <Dialog
      open={open}
      onOpenChange={(v) => {
        setOpen(v);
        if (!v) resetForm();
      }}
    >
      <DialogTrigger asChild>
        <Button size="sm" className="gap-1.5">
          <Plus className="h-4 w-4" />
          Generate Report
        </Button>
      </DialogTrigger>

      <DialogContent className="sm:max-w-md">
        <DialogHeader>
          <DialogTitle>Generate Customer Report</DialogTitle>
          <DialogDescription>
            Choose a report type and optional date range to generate a new
            customer report.
          </DialogDescription>
        </DialogHeader>

        <div className="space-y-4 py-2">
          {/* Report Type */}
          <div className="space-y-1.5">
            <Label htmlFor="report-type">Report Type</Label>
            <Select
              value={reportType === "" ? "" : String(reportType)}
              onValueChange={(val) =>
                setReportType(Number(val) as CustomerReportType)
              }
            >
              <SelectTrigger id="report-type">
                <SelectValue placeholder="Select report type…" />
              </SelectTrigger>
              <SelectContent>
                {ALL_REPORT_TYPES.map((t) => (
                  <SelectItem key={t} value={String(t)}>
                    {REPORT_TYPE_LABELS[t]}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
            {reportType !== "" && (
              <p className="text-xs text-muted-foreground">
                {REPORT_TYPE_DESCRIPTIONS[reportType as CustomerReportType]}
              </p>
            )}
          </div>

          {/* Period (only for types that need it) */}
          {needsPeriod && (
            <div className="grid grid-cols-2 gap-3">
              <div className="space-y-1.5">
                <Label htmlFor="period-start">
                  <CalendarIcon className="inline h-3 w-3 mr-1" />
                  Period Start
                </Label>
                <Input
                  id="period-start"
                  type="date"
                  value={periodStart}
                  onChange={(e) => setPeriodStart(e.target.value)}
                />
              </div>
              <div className="space-y-1.5">
                <Label htmlFor="period-end">
                  <CalendarIcon className="inline h-3 w-3 mr-1" />
                  Period End
                </Label>
                <Input
                  id="period-end"
                  type="date"
                  value={periodEnd}
                  min={periodStart}
                  onChange={(e) => setPeriodEnd(e.target.value)}
                />
              </div>
            </div>
          )}
        </div>

        <DialogFooter className="gap-2">
          <Button
            variant="outline"
            onClick={() => {
              setOpen(false);
              resetForm();
            }}
            disabled={mutation.isPending}
          >
            Cancel
          </Button>
          <Button
            onClick={handleSubmit}
            disabled={!isValid || mutation.isPending}
          >
            {mutation.isPending && (
              <Loader2 className="mr-2 h-4 w-4 animate-spin" />
            )}
            Generate
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
