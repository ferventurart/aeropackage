using System;
using System.Drawing;
using AeroPackage.Application.Common.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace AeroPackage.Infrastructure.Services;

public class ExcelService : IExcelService
{
    public async Task<byte[]> GenerateExcelFile(string worksheets)
    {
        var data = new List<string[]>
            {
                new string[]{"Juan", "Tienda A", "5", "500", "50", "12345"},
                new string[]{"Juan", "Tienda B", "3", "300", "30", "12346"},
                new string[]{"Maria", "Tienda C", "4", "400", "40", "12347"},
                new string[]{"Maria", "Tienda D", "2", "200", "20", "12348"}
            };

        var clientes = data.GroupBy(x => x[0]);

        using (var package = new ExcelPackage())
        {
            foreach (var cliente in clientes)
            {
                var worksheet = package.Workbook.Worksheets.Add(cliente.Key);

                // Headers
                worksheet.Cells[1, 1].Value = "Tienda";
                worksheet.Cells[1, 2].Value = "Numero de Articulos";
                worksheet.Cells[1, 3].Value = "Total";
                worksheet.Cells[1, 4].Value = "Impuesto";
                worksheet.Cells[1, 5].Value = "Numero de Tracking";

                // Format Headers
                var headerRange = worksheet.Cells["A1:E1"];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                headerRange.Style.Font.Color.SetColor(Color.White);

                // Rows
                decimal total = 0;
                int articulos = 0;
                decimal impuesto = 0;

                for (int i = 0; i < cliente.Count(); i++)
                {
                    worksheet.Cells[i + 2, 1].Value = cliente.ElementAt(i)[1];
                    worksheet.Cells[i + 2, 2].Value = int.Parse(cliente.ElementAt(i)[2]);
                    worksheet.Cells[i + 2, 3].Value = decimal.Parse(cliente.ElementAt(i)[3]);
                    worksheet.Cells[i + 2, 4].Value = decimal.Parse(cliente.ElementAt(i)[4]);
                    worksheet.Cells[i + 2, 5].Value = cliente.ElementAt(i)[5];

                    total += decimal.Parse(cliente.ElementAt(i)[3]);
                    articulos += int.Parse(cliente.ElementAt(i)[2]);
                    impuesto += decimal.Parse(cliente.ElementAt(i)[4]);
                }

                // Footer
                worksheet.Cells[cliente.Count() + 2, 1].Value = "Total";
                worksheet.Cells[cliente.Count() + 2, 2].Value = articulos;
                worksheet.Cells[cliente.Count() + 2, 3].Value = total;
                worksheet.Cells[cliente.Count() + 2, 4].Value = impuesto;

                // Format Footer
                var footerRange = worksheet.Cells[$"A{cliente.Count() + 2}:E{cliente.Count() + 2}"];
                footerRange.Style.Font.Bold = true;
                footerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                footerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(192, 192, 192));

                // Autosize columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }

            // Save the Excel file
            return await package.GetAsByteArrayAsync();
        }

    }
}

