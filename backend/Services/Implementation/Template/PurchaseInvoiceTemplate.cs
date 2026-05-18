using backend.Data.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace backend.Services.Implementation.Template;

public class PurchaseInvoiceTemplate(PurchaseInvoice model) : IDocument
{
    private PurchaseInvoice Invoice { get; } = model;

    public DocumentMetadata Metadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
          container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span("/");
                    x.TotalPages();
                });
            });
    }


    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .Text($"Invoice #{Invoice.InvoiceNumber}")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                column.Item().Text(text =>
                {
                    text.Span("Issue date: ").SemiBold();
                    text.Span($"{Invoice.IssueDate:d}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Due date: ").SemiBold();
                    text.Span($"{Invoice.IssueDate:d}");
                });
            });

            row.ConstantItem(100).Height(50).Placeholder();
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);
            column.Item().Row(row =>
            {
                row.RelativeItem().Component(new VendorTemplate("Vendor Information", Invoice.Vendor!));
            });
            column.Item().Element(ComposeTable);

            var totalPrice = Invoice?.Part.CostPrice * Invoice?.Part.StockQuantity;
            column.Item().AlignRight().Text($"Grand total: {totalPrice}$").FontSize(14).SemiBold();


        });
    }

    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).PaddingBottom(5).Text("Part Name");
                header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                header.Cell().Element(CellStyle).AlignRight().Text("Total");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });


                    table.Cell().Element(CellStyle).Text("1");
                    table.Cell().Element(CellStyle).Text(Invoice.Part.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{Invoice.Part.CostPrice}$");
                    table.Cell().Element(CellStyle).AlignRight().Text(Invoice.Part.StockQuantity);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{Invoice.Part.CostPrice * Invoice.Part.StockQuantity}$");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                            .PaddingVertical(5);
                    }

        });
    }


}