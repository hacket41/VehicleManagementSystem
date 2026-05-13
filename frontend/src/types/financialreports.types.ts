export type ReportType = "Daily" | "Monthly" | "Yearly";

export interface GenerateFinancialReportRequest{
  type: ReportType
  date: string
}

export
