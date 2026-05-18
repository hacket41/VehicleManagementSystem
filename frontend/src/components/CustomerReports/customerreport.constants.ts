import { TrendingUp, Users, CreditCard } from "lucide-react";
import { CustomerReportType } from "#/types/customerreport.types";

export const FILTER_OPTIONS = [
  { value: "all", label: "All Types" },
  { value: String(CustomerReportType.TopSpenders), label: "Top Spenders" },
  {
    value: String(CustomerReportType.RegularCustomers),
    label: "Regular Customers",
  },
  {
    value: String(CustomerReportType.PendingCredits),
    label: "Pending Credits",
  },
];

export const REPORT_TYPE_META: Record<
  CustomerReportType,
  {
    label: string;
    variant: "default" | "secondary" | "destructive" | "outline";
    icon: React.ElementType;
  }
> = {
  [CustomerReportType.TopSpenders]: {
    label: "Top Spenders",
    variant: "default",
    icon: TrendingUp,
  },
  [CustomerReportType.RegularCustomers]: {
    label: "Regular Customers",
    variant: "secondary",
    icon: Users,
  },
  [CustomerReportType.PendingCredits]: {
    label: "Pending Credits",
    variant: "destructive",
    icon: CreditCard,
  },
};
