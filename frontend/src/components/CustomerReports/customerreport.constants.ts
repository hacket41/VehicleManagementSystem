import { CustomerReportType } from "#/types/customerreport.types";

export const REPORT_TYPE_LABELS: Record<CustomerReportType, string> = {
  [CustomerReportType.TopSpenders]: "Top Spenders",
  [CustomerReportType.RegularCustomers]: "Regular Customers",
  [CustomerReportType.PendingCredits]: "Pending Credits",
};

export const REPORT_TYPE_DESCRIPTIONS: Record<CustomerReportType, string> = {
  [CustomerReportType.TopSpenders]:
    "Customers ranked by total amount spent within the period.",
  [CustomerReportType.RegularCustomers]:
    "Customers with the highest number of purchases within the period.",
  [CustomerReportType.PendingCredits]:
    "Customers with outstanding unpaid credits, sorted by oldest due date.",
};

export const REPORT_TYPE_COLORS: Record<
  CustomerReportType,
  { badge: string; accent: string }
> = {
  [CustomerReportType.TopSpenders]: {
    badge: "bg-amber-100 text-amber-800 border-amber-200",
    accent: "text-amber-600",
  },
  [CustomerReportType.RegularCustomers]: {
    badge: "bg-blue-100 text-blue-800 border-blue-200",
    accent: "text-blue-600",
  },
  [CustomerReportType.PendingCredits]: {
    badge: "bg-rose-100 text-rose-800 border-rose-200",
    accent: "text-rose-600",
  },
};

export const ALL_REPORT_TYPES = [
  CustomerReportType.TopSpenders,
  CustomerReportType.RegularCustomers,
  CustomerReportType.PendingCredits,
] as const;
