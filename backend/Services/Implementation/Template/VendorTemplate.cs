using backend.Data.DTO.Response;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace backend.Services.Implementation.Template;

public class VendorTemplate(string title, VendorInfoForInvoice vendor) : IComponent
{
    private string Title { get; } = title;
    private VendorInfoForInvoice Vendor { get; } = vendor;

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(2);
            column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

            column.Item().Text(Vendor.Name);
            column.Item().Text(Vendor.ContactPerson);
            column.Item().Text(Vendor.Email);
            column.Item().Text(Vendor.Phone);
            column.Item().Text(Vendor.Address);
        });
    }

}