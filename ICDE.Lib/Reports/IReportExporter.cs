using ICDE.Lib.Validator;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ICDE.Lib.Reports;
internal interface IReportExporter
{
    byte[] ExportData(List<ValidationResult> validationResults);
}

internal class PdfReportExporter : IReportExporter
{
    public byte[] ExportData(List<ValidationResult> validationResults)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        using var ms = new MemoryStream();
        {
            var document = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(25);
                    page.Content()
                        .DefaultTextStyle(style => style.FontSize(10))
                        .Element(container =>
                            container.PaddingVertical(10).Column(column =>
                            {
                                bool success = validationResults.All(x => x.Success);
                                string successText = success == true ? "Geslaagd" : "Gefaald";
                                column.Spacing(5);
                                AddTitleText(column, $"Validatie is: {successText}");
                                AddValidationResults(column, validationResults);
                            }));
                    page.Footer().AlignCenter().Text(_ =>
                    {
                        _.CurrentPageNumber();
                        _.Span(" / ");
                        _.TotalPages();
                    });
                });
            });
            document.GeneratePdf(ms);
            return ms.ToArray();
        }
    }

    private static void AddTitleText(ColumnDescriptor column, string title)
    {
        column.Item().Text(text =>
        {
            text.AlignCenter();
            text.Span(title, TextStyle.Default);
            text.DefaultTextStyle(style => style.FontSize(28));
        });
    }

    private void AddValidationResults(ColumnDescriptor column, List<ValidationResult> results)
    {
        column.Item().Element(tableContainer =>
        {
            tableContainer
                .Padding(10)
                .MinimalBox()
                .Border(1)
                .Table(table =>
                {
                    table.Header(h =>
                    {
                        h.Cell().Element(HeaderCellStyle).Text("Succes");
                        h.Cell().Element(HeaderCellStyle).Text("Bericht");
                    });

                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(100);
                        columns.RelativeColumn();
                    });

                    foreach (var result in results)
                    {
                        foreach (var message in result.Messages)
                        {
                            table.Cell().Element(CellStyle).Text(result.Success);
                            table.Cell().Element(CellStyle).Text(message);
                        }
                    }
                });
        });
    }
    private IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                => container
                    .Border(1)
                    .BorderColor(Colors.Grey.Lighten1)
                    .Background(backgroundColor)
                    .PaddingVertical(2)
                    .PaddingHorizontal(5)
                    .AlignMiddle();

    private IContainer CellStyle(IContainer container)
        => DefaultCellStyle(container, Colors.White).ShowOnce();

    private IContainer HeaderCellStyle(IContainer container)
        => DefaultCellStyle(container, Colors.Grey.Lighten3);

}