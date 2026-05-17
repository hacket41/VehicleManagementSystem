export type ReportType = "Daily" | "Monthly" | "Yearly";

export interface GenerateFinancialReportRequest {
  type: ReportType;
  date: string;
}

export interface FinancialReport {
  id: number;
  type: ReportType;
  periodStart: string;
  periodEnd: string;
  totalSalesRevenue: number;
  totalPurchaseCost: number;
  totalDiscountGiven: number;
  grossProfit: number;
  totalCreditOutstanding: number;
  totalTransactions: number;
  totalUnitsSold: number;
  generatedAt: string;
  generatedBy: string;
}
