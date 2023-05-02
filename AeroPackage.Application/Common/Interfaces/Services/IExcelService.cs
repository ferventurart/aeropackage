using System;
namespace AeroPackage.Application.Common.Interfaces.Services;

public interface IExcelService
{
    Task<byte[]> GenerateExcelFile(string worksheets);
}

