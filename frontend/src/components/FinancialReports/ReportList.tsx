import { useQuery } from "@tanstack/react-query";
import { getFinancialReport } from "#/api/financialreport.api";
import type { FinancialReport } from "#/types/financialreports.types";
import { Badge } from "@/components/ui/badge";

interface Props {
  onSelect: (id: number) => void;
}

export function ReportList({ onSelect }: Props) {
  const { data, isLoading } = useQuery(getFinancialReport());

  return (
    <div className="border rounded-lg p-4 space-y-3">
      <h2 className="text-lg font-semibold">Reports</h2>

      {isLoading && <p>Loading...</p>}

      {data?.map((r: FinancialReport) => (
        <div
          key={r.id}
          onClick={() => onSelect(r.id)}
          className="p-3 border rounded-md hover:bg-muted cursor-pointer flex justify-between"
        >
          <div>
            <p className="font-medium">
              {r.type} <Badge variant="secondary">Profit {r.grossProfit}</Badge>
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
