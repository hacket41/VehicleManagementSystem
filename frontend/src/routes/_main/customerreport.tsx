import { useState } from "react";
import { Separator } from "#/components/ui/separator";
import { Card, CardContent } from "#/components/ui/card";
import { type CustomerReportDto } from "#/types/customerreport.types";
import { CustomerReportListPanel } from "#/components/CustomerReports/CustomerReportListPanel";
import { CustomerReportDetailPanel } from "#/components/CustomerReports/CustomerReportDetailPanel";

export default function CustomerReportPage() {
  const [selectedReport, setSelectedReport] =
    useState<CustomerReportDto | null>(null);

  return (
    <div className="flex h-full flex-col gap-6 p-6">
      <div>
        <h1 className="text-2xl font-semibold tracking-tight">
          Customer Reports
        </h1>
        <p className="mt-1 text-sm text-muted-foreground">
          Browse and inspect generated customer reports.
        </p>
      </div>

      <Separator />

      <div className="grid flex-1 grid-cols-1 gap-6 overflow-hidden md:grid-cols-[300px_1fr]">
        <Card className="flex flex-col overflow-hidden">
          <CardContent className="flex flex-1 flex-col overflow-hidden p-4">
            <CustomerReportListPanel
              selectedId={selectedReport?.id ?? null}
              onSelect={setSelectedReport}
            />
          </CardContent>
        </Card>

        <Card className="flex flex-col overflow-hidden">
          <CardContent className="flex flex-1 flex-col overflow-hidden p-4">
            <CustomerReportDetailPanel report={selectedReport} />
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
