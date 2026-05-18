export enum CustomerReportType {
  TopSpenders = 0,
  RegularCustomers = 1,
  PendingCredits = 2,
}

export interface CustomerReportRowDto {
  customerId: number;
  fullName: string;
  email: string;
  phone: string;
  totalPurchase: number;
  totalSpent?: number;
  outStandingCredit?: number;
  oldestUnpaidCreditDate?: Date;
}

export interface CustomerReportDto {
  id: number;
  type: string;
  periodStart?: string;
  periodEnd?: string;
  generatedAt: string;
  generatedBy: string;
  rows: CustomerReportRowDto[];
}
