using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Entities;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.PackageAggregate.ValueObjects;

namespace AeroPackage.Application.Common.Interfaces.Persistence;

public interface IPackageRepository
{
    Task<int> GetLastInsertedId();

    Task<PaginatedResult<Package>> GetPackagesByPeriod(DateTime from, DateTime to, PackageStatus status, List<string> stores, int pageSize, int pageNumber);

    Task<List<Package>> GetPackagesByPeriodAndStore(DateTime from, DateTime to, List<string> stores);

    Task<PaginatedResult<Package>> GetPackagesOfCustomer(CustomerId customerId, DateTime from, DateTime to, int pageSize, int pageNumber);

    CustomerId GetCustomerOwnerOfPackage(PackageId packageId);

    bool ExistsWithTrackingNumber(string trackingNumber);

    bool Exists(PackageId packageId);

    Package? GetPackageById(PackageId packageId);

    Package? GetPackageByOwnTrackingNumber(OwnTrackingNumber ownTrackingNumber);

    void Add(Package package);

    void Update(Package package);

    void Delete(Package package);
}

