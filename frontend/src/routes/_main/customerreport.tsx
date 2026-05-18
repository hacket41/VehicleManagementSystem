import { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";

import { queryClient } from "#/lib/queryClient";
import { getCustomerReports } from "#/api/customerreport.api";
import type { CustomerReportDto } from "#/types/customerreport.types";
import { CustomerReportListPanel } from "#/components/CustomerReports/CustomerReportListPanel";
import { CustomerReportDetailPanel } from "#/components/CustomerReports/CustomerReportDetailPanel";

export const Route = createFileRoute("/_main/customerreport")({
  component: CustomerReportPage,
  loader: async () => await queryClient.ensureQueryData(getCustomerReports()),
});

function CustomerReportPage() {
  const [selectedReport, setSelectedReport] =
    useState<CustomerReportDto | null>(null);

  const { data: reports } = useQuery(getCustomerReports());
  if (!reports) return null;

  return (
    <div className="flex h-[calc(100vh-4rem)] overflow-hidden">
      <div className="w-80 shrink-0 flex flex-col overflow-hidden">
        <CustomerReportListPanel
          selectedId={selectedReport?.id ?? null}
          onSelect={setSelectedReport}
        />
      </div>
      <div className="flex-1 flex flex-col overflow-hidden bg-muted/30">
        <CustomerReportDetailPanel reportId={selectedReport?.id ?? null} />
      </div>
    </div>
  );
}
