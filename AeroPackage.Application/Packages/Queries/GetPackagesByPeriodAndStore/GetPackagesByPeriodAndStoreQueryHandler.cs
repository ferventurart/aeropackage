using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Customers.Queries.GetCustomers;
using AeroPackage.Contracts.Package;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Enums;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodAndStore;

public class GetPackagesByPeriodAndStoreQueryHandler : IRequestHandler<GetPackagesByPeriodAndStoreQuery, ErrorOr<List<DragPackageResponse>>>
{
    private readonly IPackageRepository _packageRepository;

    public GetPackagesByPeriodAndStoreQueryHandler(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<ErrorOr<List<DragPackageResponse>>> Handle(GetPackagesByPeriodAndStoreQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        List<string> stores = new List<string>();

        foreach (string elemento in query.Stores.Split(','))
        {
            stores.Add(elemento.Trim());
        }

        var packages = await _packageRepository
            .GetPackagesByPeriodAndStore(query.From, query.To, stores);

        var response = packages.Select(s => new DragPackageResponse(s.OwnTrackingNumber.Value, s.Description, s.QuantityArticles, s.Store, s.Status.Name)).ToList();

        return response;
    }
}

