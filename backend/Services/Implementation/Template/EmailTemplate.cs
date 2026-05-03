using backend.Data.Entities;

namespace backend.Services.Implementation.Template;

public static class EmailTemplates
{
    public static string LowStockAlert(IEnumerable<Part> parts) =>
        $"""
        <html><body style="font-family:Arial,sans-serif;color:#333;max-width:600px;margin:auto">
          <div style="background:#c0392b;padding:20px;border-radius:8px 8px 0 0">
            <h2 style="color:#fff;margin:0">Low Stock Alert</h2>
          </div>
          <div style="padding:24px;border:1px solid #ddd;border-top:none;border-radius:0 0 8px 8px">
            <p>The following parts have fallen below their minimum stock threshold:</p>
            <table width="100%" cellpadding="10" cellspacing="0"
                   style="border-collapse:collapse;margin-top:12px">
              <thead>
                <tr style="background:#f2f2f2">
                  <th align="left" style="border-bottom:2px solid #c0392b">Part</th>
                  <th align="left" style="border-bottom:2px solid #c0392b">Part #</th>
                  <th align="center" style="border-bottom:2px solid #c0392b">In Stock</th>
                  <th align="center" style="border-bottom:2px solid #c0392b">Threshold</th>
                </tr>
              </thead>
              <tbody>
                {string.Join("", parts.Select((p, i) => $"""
                  <tr style="background:{(i % 2 == 0 ? "#fff" : "#fafafa")}">
                    <td style="border-bottom:1px solid #eee">{p.Name}</td>
                    <td style="border-bottom:1px solid #eee">{p.PartNumber}</td>
                    <td align="center" style="border-bottom:1px solid #eee;color:#c0392b;font-weight:bold">{p.StockQuantity}</td>
                    <td align="center" style="border-bottom:1px solid #eee">{p.LowStockThreshold}</td>
                  </tr>
                """))}
              </tbody>
            </table>
            <p style="margin-top:20px;font-size:13px;color:#888">
              This is an automated alert from the AutoParts system. Please restock soon.
            </p>
          </div>
        </body></html>
        """;

    public static string CreditReminder(Sale sale, int daysOverdue) =>
        $"""
        <html><body style="font-family:Arial,sans-serif;color:#333;max-width:600px;margin:auto">
          <div style="background:#2980b9;padding:20px;border-radius:8px 8px 0 0">
            <h2 style="color:#fff;margin:0">Payment Reminder</h2>
          </div>
          <div style="padding:24px;border:1px solid #ddd;border-top:none;border-radius:0 0 8px 8px">
            <p>Dear <strong>{sale.Customer.FirstName} {sale.Customer.LastName}</strong>,</p>
            <p>This is a friendly reminder that the following invoice remains unpaid:</p>
            <table width="100%" cellpadding="10" cellspacing="0"
                   style="border-collapse:collapse;background:#f9f9f9;border-radius:6px;margin:16px 0">
              <tr>
                <td style="border-bottom:1px solid #eee;color:#666">Invoice Number</td>
                <td style="border-bottom:1px solid #eee"><strong>{sale.InvoiceNumber}</strong></td>
              </tr>
              <tr>
                <td style="border-bottom:1px solid #eee;color:#666">Amount Due</td>
                <td style="border-bottom:1px solid #eee">
                  <strong style="color:#c0392b;font-size:18px">{sale.TotalAmount:C}</strong>
                </td>
              </tr>
              <tr>
                <td style="border-bottom:1px solid #eee;color:#666">Due Date</td>
                <td style="border-bottom:1px solid #eee">
                  {(sale.CreditDueDate ?? sale.SaleDate.AddMonths(1)):MMMM dd, yyyy}
                </td>
              </tr>
              <tr>
                <td style="color:#666">Days Overdue</td>
                <td><strong style="color:#e74c3c">{daysOverdue} days</strong></td>
              </tr>
            </table>
            <p>Please settle this balance at your earliest convenience to avoid any service interruptions.</p>
            <p style="margin-top:24px;font-size:13px;color:#888">
              If you believe this is an error or have already made a payment, please contact us immediately.<br>
              This is an automated message — please do not reply directly to this email.
            </p>
          </div>
        </body></html>
        """;
}