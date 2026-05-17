import { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";
import { queryClient } from "#/lib/queryClient";
import { GenerateReportCard } from "#/components/FinancialReports/GenerateReportCard";
import { ReportList } from "#/components/FinancialReports/ReportList";
import { ReportDetailsDialog } from "#/components/FinancialReports/ReportDetailsDialog";

import { getFinancialReport } from "#/api/financialreport.api";

export const Route = createFileRoute("/_main/financialreport")({
  component: RouteComponent,
  loader: async () => await queryClient.ensureQueryData(getFinancialReport()),
});

function RouteComponent() {
  const [selectedId, setSelectedId] = useState<number | null>(null);

  const { data: reports } = useQuery(getFinancialReport());

  if (!reports) return null;

  return (
    <div className="space-y-6 p-6">
      <div>
        <h1 className="text-2xl font-bold">Financial Reports</h1>

        <p className="text-muted-foreground">
          Manage and analyze business financial reports
        </p>
      </div>

      <GenerateReportCard />

      <ReportList reports={reports} onSelect={setSelectedId} />

      <ReportDetailsDialog
        id={selectedId}
        onClose={() => setSelectedId(null)}
      />
    </div>
  );
}
