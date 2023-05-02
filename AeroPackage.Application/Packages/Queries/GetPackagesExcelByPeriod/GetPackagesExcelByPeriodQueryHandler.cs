using System;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Interfaces.Services;
using AeroPackage.Domain.PackageAggregate.Enums;
using ErrorOr;
using MediatR;
using OfficeOpenXml;

namespace AeroPackage.Application.Packages.Queries.GetPackagesExcelByPeriod;

public class GetPackagesExcelByPeriodQueryHandler : IRequestHandler<GetPackagesExcelByPeriodQuery, ErrorOr<byte[]>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IExcelService _excelService;

    public GetPackagesExcelByPeriodQueryHandler(IPackageRepository packageRepository, IExcelService excelService)
    {
        _packageRepository = packageRepository;
        _excelService = excelService;
    }

    public async Task<ErrorOr<byte[]>> Handle(GetPackagesExcelByPeriodQuery query, CancellationToken cancellationToken)
    {
        var packages = await _packageRepository
            .GetPackagesByPeriod(query.from, query.to,
           PackageStatus.FromName(query.status), query.pageSize, query.pageNumber);


        byte[] excelFile = await _excelService.GenerateExcelPackagesByPeriodAndStatus("Prueba", packages.Results);

        return excelFile;
    }
}

