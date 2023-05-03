using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByCustomer;

public class GetPackageByCustomerQueryHandler : IRequestHandler<GetPackageByCustomerQuery, ErrorOr<PaginatedResult<Package>>>
{
    private readonly IPackageRepository _packageRepository;

    public GetPackageByCustomerQueryHandler(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<ErrorOr<PaginatedResult<Package>>> Handle(GetPackageByCustomerQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var packages = await _packageRepository.GetPackagesOfCustomer(
            CustomerId.Create(query.Id),
            query.From,
            query.To, query.PageSize, query.PageNumber);

        return packages;
    }
}