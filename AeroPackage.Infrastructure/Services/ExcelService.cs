using System;
using AeroPackage.Application.Common.Interfaces.Services;
using OfficeOpenXml;

namespace AeroPackage.Infrastructure.Services;

public class ExcelService : IExcelService
{
    public async Task<byte[]> GenerateExcelFile(string worksheets)
    {
        // Crea el objeto ExcelPackage
        ExcelPackage excel = new ExcelPackage();

        // Agrega una hoja de trabajo
        var worksheet = excel.Workbook.Worksheets.Add(worksheets);

        // Agrega contenido a la hoja de trabajo
        worksheet.Cells["A1"].Value = "Hola";
        worksheet.Cells["B1"].Value = "mundo!";

        return await excel.GetAsByteArrayAsync();
    }
}

