using System;
using AeroPackage.Domain.PackageAggregate;

namespace AeroPackage.Application.Common.Interfaces.Services;

public interface IExcelService
{
    Task<byte[]> GenerateExcelFile(string worksheets);

    Task<byte[]> GenerateExcelPackagesByPeriodAndStatus(string worksheets, List<Package> packages);
}

