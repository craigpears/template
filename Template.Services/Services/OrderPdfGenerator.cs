using Humanizer;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Template.Models;
using Template.Models.Extensions;
using Template.Models.Models;

namespace Template.Services.Services;

public class OrderPdfGenerator
{
    public static byte[] GenerateOrderPdf(Appointment appointment)
    {
        var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(6, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8));
        
                    page.Header().Text("ORDER FORM").AlignCenter().Bold().FontSize(12);
        
                    page.Content()
                        .PaddingVertical(2, Unit.Millimetre)
                        .Column(outerColumn =>
                        {
                            outerColumn.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1.2F);
                                    columns.RelativeColumn(1);
                                });

                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("CUSTOMER DETAILS").Bold();
                                    column.Item().Text($"{appointment.Title} {appointment.Surname}");
                                    column.Item().Text($"{appointment.CompanyName}");
                                    column.Item().Text($"{appointment.Address1}");
                                    column.Item().Text($"{appointment.Address2}");
                                    column.Item().Text($"{appointment.Town}");
                                    column.Item().Text($"{appointment.County}");
                                    column.Item().Text($"{appointment.Postcode}");
                                });

                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("DELIVERY ADDRESS").Bold();
                                    if (appointment.DeliveryAddressSameAsOrderAddress)
                                    {
                                        column.Item().Text($"Same as order address");
                                    }
                                    else
                                    {
                                        column.Item().Text($"{appointment.DeliveryAddress1}");
                                        column.Item().Text($"{appointment.DeliveryAddress2}");
                                        column.Item().Text($"{appointment.DeliveryTown}");
                                        column.Item().Text($"{appointment.DeliveryCounty}");
                                        column.Item().Text($"{appointment.DeliveryPostcode}");
                                    }
                                });

                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("CUSTOMER CONTACT DETAILS").Bold();
                                    column.Item().Text($"Customer Telephone: {appointment.Telephone}");
                                    column.Item().Text($"Customer Mobile: {appointment.AlternativeTelephone}");
                                    column.Item().Text($"Customer Email: {appointment.Email}");
                                });
                            });     
                            
                            outerColumn.Item().PaddingVertical(5, Unit.Millimetre).Text("ORDER DETAILS").AlignCenter().Bold().FontSize(12);
                            
                            outerColumn.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(0.8f);
                                    columns.RelativeColumn(1.2f);
                                    columns.RelativeColumn(1.2f);
                                    columns.RelativeColumn(0.8f);
                                    columns.RelativeColumn(7.85f);
                                });

                                table.Cell().Text("Room Ref").Bold();
                                table.Cell().Text("Product Type").Bold();
                                table.Cell().Text("Measurements").Bold();
                                table.Cell().Text("Size Type").Bold();
                                table.Cell().Text("Details").Bold();

                                var productTable = appointment.ToProductTable();
                                foreach (var section in productTable.Sections)
                                foreach (var line in section.Lines)
                                {
                                    table.Cell().Text(line.RoomRef);
                                    table.Cell().Text(line.ProductType.GetDisplayName());
                                    table.Cell().Text($"{line.Width} x {line.Drop}");
                                    table.Cell().Text(line.SizeType);
                                    table.Cell().Table(detailsTable =>
                                    {
                                        detailsTable.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1);
                                            columns.RelativeColumn(1);
                                            columns.RelativeColumn(1);
                                        });

                                        var ignoredProperties = AppointmentExtensions.GetPropertyNames<QuoteLine>(
                                            l => l.RoomRef,
                                            l => l.ProductType,
                                            l => l.Measurements,
                                            l => l.SizeType
                                        );
                                        var propertyNames = section.PropertyNames.Except(ignoredProperties);
                                        foreach (var propertyName in propertyNames)
                                        {
                                            uint columnSpan = 1;
                                            if (propertyName == nameof(QuoteLine.Description))
                                            {
                                                columnSpan = 3;
                                            }
                                            detailsTable.Cell().ColumnSpan(columnSpan).Text(text =>
                                            {
                                                text.Span($"{propertyName.Humanize()}: ");
                                                text.Span(line.GetPropertyValue(propertyName)?.ToString()).Bold();
                                            });
                                        }
                                    });
                                }
                            });
                            
                            outerColumn.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            
                            outerColumn.Item().PaddingVertical(10, Unit.Millimetre).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                });

                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("SPECIAL INSTRUCTIONS").Bold();
                                    column.Item().Text(appointment.SpecialInstructions);
                                });

                                table.Cell().Column(column => {});

                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("");
                                    column.Item().Text(text =>
                                    {
                                        text.Span("PRICE QUOTED: ");
                                        text.Span($"£{appointment.PriceQuoted}").Bold();
                                    });
                                });
                            });
                        });
                });
            }).GeneratePdf();
        return pdfBytes;
    }
}