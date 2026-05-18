import { queryOptions } from "@tanstack/react-query";
import { apiFetch } from "#/lib/api";
import {
  CustomerReportType,
  type CustomerReportDto,
  type GenerateCustomerReportRequest,
} from "#/types/customerreport.types";

export const generateCustomerReport = async (
  payload: GenerateCustomerReportRequest,
) => {
  return apiFetch<CustomerReportDto>("/api/CustomerReport/generate", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });
};

export const getCustomerReports = (type?: CustomerReportType) => {
  const query = type !== undefined ? `?type=${type}` : "";
  return queryOptions({
    queryKey: ["customer-reports", type],
    queryFn: async () =>
      apiFetch<CustomerReportDto[]>(`/api/CustomerReport${query}`, {
        method: "GET",
      }),
  });
};

export const getCustomerReportById = (id: number) => {
  return queryOptions({
    queryKey: ["customer-reports", id],
    queryFn: async () =>
      apiFetch<CustomerReportDto>(`/api/CustomerReport/${id}`, {
        method: "GET",
      }),
  });
};
