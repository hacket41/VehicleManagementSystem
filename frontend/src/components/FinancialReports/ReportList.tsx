import type { FinancialReport } from "#/types/financialreports.types";
import { Badge } from "@/components/ui/badge";
import { Button } from "../ui/button";
import { useMutation } from "@tanstack/react-query";
import { queryClient } from "#/lib/queryClient";
import { deleteFinancialReport } from "#/api/financialreport.api";
interface Props {
  reports: FinancialReport[];
  onSelect: (id: number) => void;
}

export function ReportList({ reports, onSelect }: Props) {
  const deleteMutation = useMutation({
    mutationFn: deleteFinancialReport,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["financialreport"],
      });
    },
  });
  return (
    <div className="border rounded-lg p-4 space-y-3">
      <h2 className="text-lg font-semibold">Reports</h2>

      {reports.map((r) => (
        <div
          key={r.id}
          onClick={() => onSelect(r.id)}
          className="p-3 border rounded-md hover:bg-muted cursor-pointer flex justify-between"
        >
          <div>
            <p className="font-medium">
              {r.type}
              <Badge variant="secondary" className="ml-2">
                Profit {r.grossProfit}
              </Badge>

              <Button
                variant="destructive"
                size="sm"
                onClick={() => {
                  if (confirm("Delete this report?")) {
                    deleteMutation.mutate(r.id);
                  }
                }}
              >
                {" "}
                Delete{" "}
              </Button>
            </p>

            <p className="text-sm text-muted-foreground">
              Sales: {r.totalSalesRevenue} | Units: {r.totalUnitsSold}
            </p>
          </div>

          <Badge>{new Date(r.generatedAt).toLocaleDateString()}</Badge>
        </div>
      ))}
    </div>
  );
}
