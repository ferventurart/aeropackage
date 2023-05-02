﻿using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Customers.Queries.GetCustomers;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Enums;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodStatusAndStore;

public class GetPackagesByPeriodStatusAndStoreQueryHandler : IRequestHandler<GetPackagesByPeriodStatusAndStoreQuery, ErrorOr<PaginatedResult<Package>>>
{
    private readonly IPackageRepository _packageRepository;

    public GetPackagesByPeriodStatusAndStoreQueryHandler(IPackageRepository packageRepository)
    { 
        _packageRepository = packageRepository;
    }

    public async Task<ErrorOr<PaginatedResult<Package>>> Handle(GetPackagesByPeriodStatusAndStoreQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        List<string> stores = new List<string>();

        foreach (string elemento in query.stores.Split(','))
        {
            stores.Add(elemento.Trim());
        }

        var packages = await _packageRepository
            .GetPackagesByPeriod(query.from, query.to,
            PackageStatus.FromName(query.status), stores, query.pageSize, query.pageNumber);

        return packages;
    }
}

