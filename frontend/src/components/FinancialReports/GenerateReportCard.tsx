import { useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/select";
import { generateFinancialReport } from "#/api/financialreport.api";
import type { ReportType } from "#/types/financialreports.types";

export function GenerateReportCard() {
  const qc = useQueryClient();
  const [type, setType] = useState<ReportType>("Daily");
  const [date, setDate] = useState("");

  const mutation = useMutation({
    mutationFn: generateFinancialReport,
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["financial-reports"] });
      setDate("");
    },
  });

  const handleGenerate = () => {
    if (!date) return;
    mutation.mutate({ type, date });
  };

  return (
    <div className="p-4 border rounded-lg space y-3">
      <h2 className="text-lg font-semibold">Generate Report</h2>

      <div className="flex gap-3 flex-col md:flex-row">
        <Select value={type} onValueChange={(v) => setType(v as ReportType)}>
          <SelectTrigger className="w-[180px]">
            <SelectValue />
          </SelectTrigger>

          <SelectContent>
            <SelectItem value="Daily">Daily</SelectItem>
            <SelectItem value="Monthly">Monthly</SelectItem>
            <SelectItem value="Yearly">Yearly</SelectItem>
          </SelectContent>
        </Select>

        <Input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          className="w-[200px]"
        />

        <Button onClick={handleGenerate} disabled={mutation.isPending}>
          {mutation.isPending ? "Generating..." : "Generate"}
        </Button>
      </div>
    </div>
  );
}
