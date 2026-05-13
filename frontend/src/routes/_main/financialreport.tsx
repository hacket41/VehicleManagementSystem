import { useState } from "react";
import { GenerateReportCard } from "#/components/FinancialReports/GenerateReportCard";
import { ReportList } from "#/components/FinancialReports/ReportList";
import { ReportDetailsDialog } from "#/components/FinancialReports/ReportDetailsDialog";

export default function FinancialReportPage() {
  const [selectedId, setSelectedId] = useState<number | null>(null);

  return (
    <div className="p-6 space-y-6">
      <div>
        <h1 className="text-2xl font-bold">Financial Reports</h1>
        <p className="text-muted-foreground">
          Admin financial analytics dashboard
        </p>
      </div>

      <GenerateReportCard />

      <ReportList onSelect={setSelectedId} />

      <ReportDetailsDialog
        id={selectedId}
        onClose={() => setSelectedId(null)}
      />
    </div>
  );
}
