import { queryOptions } from "@tanstack/react-query";
import { apiFetch } from "#/lib/api";
import type {
  GenerateFinancialReportRequest,
  FinancialReport,
  ReportType,
} from "#/types/financialreports.types";

export const generateFinancialReport = async (
  payload: GenerateFinancialReportRequest,
) => {
  return apiFetch<FinancialReport>("/api/FinancialReport/generate", {
    method: "POST",
    body: payload,
  });
};

export const getFinancialReport = (type?: ReportType) => {
  const query = type ? `?type=${type}` : ``;

  return queryOptions({
    queryKey: ["financial-reports", type],
    queryFn: async () =>
      apiFetch<FinancialReport[]>(`/api/FinancialReport${query}`, {
        method: "GET",
      }),
  });
};

export const getFinancialReportById = (id: number) => {
  return queryOptions({
    queryKey: ["financial-report", id],
    queryFn: async () =>
      apiFetch<FinancialReport>(`/api/FinancialReport/${id}`, {
        method: "GET",
      }),
  });
};

export const deleteFinancialReport = async (id: number) => {
  return apiFetch(`/api/FinancialReport/${id}`, {
    method: "DELETE",
  });
};
