using System;
using System.Net.NetworkInformation;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Entities;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Persistence.Repositories;

public class PackageRepository : IPackageRepository
{
    private readonly AeroPackageDbContext _dbContext;

    public PackageRepository(AeroPackageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Package package)
    {
        _dbContext.Add(package);
        _dbContext.SaveChanges();
    }

    public void Delete(Package package)
    {
        _dbContext.Remove(package);
        _dbContext.SaveChanges();
    }

    public bool Exists(PackageId packageId) => _dbContext.Packages.Any(package => package.Id == packageId);

    public bool ExistsWithTrackingNumber(string trackingNumber) =>
        _dbContext.Packages
                  .Any(package => package.CourierTrackingNumber == trackingNumber);

    public CustomerId GetCustomerOwnerOfPackage(PackageId packageId) =>
        _dbContext.Packages
                  .Where(w => w.Id == packageId)
                  .Select(s => s.CustomerId).Single();

    public async Task<int> GetLastInsertedId()
    {
        var lastInserted = await _dbContext.Packages.OrderByDescending(n => n.Id).FirstOrDefaultAsync();
        int nextNumber = 1;
        if (lastInserted is not null)
        {
            nextNumber = lastInserted.Id.Value + 1;
        }
        return nextNumber;
    }

    public Package? GetPackageById(PackageId packageId) =>
        _dbContext.Packages
                  .FirstOrDefault(package => package.Id == packageId);

    public Package? GetPackageByOwnTrackingNumber(OwnTrackingNumber ownTrackingNumber) =>
        _dbContext.Packages
                  .FirstOrDefault(package => package.OwnTrackingNumber == ownTrackingNumber);

    public Package? GetPackageByTrackingNumber(string trackingNumber) =>
        _dbContext.Packages
                  .FirstOrDefault(package => package.CourierTrackingNumber == trackingNumber);

    public async Task<PaginatedResult<Package>> GetPackagesByPeriod(DateTime from, DateTime to, PackageStatus status, List<string> stores, int pageSize, int pageNumber) =>
        await _dbContext.Packages
                .AsTracking()
                .Where(w => w.CreatedDateTime >= from && w.CreatedDateTime <= to && w.Status == status && stores.Contains(w.Store))
                .ToPaginatedListAsync(pageNumber, pageSize);

    public async Task<List<Package>> GetPackagesByPeriodAndStore(DateTime from, DateTime to, List<string> stores) =>
        await _dbContext.Packages
                .AsTracking()
                .Where(w => w.CreatedDateTime >= from && w.CreatedDateTime <= to && stores.Contains(w.Store))
                .ToListAsync();

    public async Task<PaginatedResult<Package>> GetPackagesOfCustomer(CustomerId customerId, DateTime from, DateTime to, int pageSize, int pageNumber) =>
        await _dbContext.Packages
                .Where(customer => customer.CustomerId == customerId)
                .Where(w => w.CreatedDateTime >= from && w.CreatedDateTime <= to)
                .ToPaginatedListAsync(pageNumber, pageSize);

    public void Update(Package package)
    {
        _dbContext.Update(package);
        _dbContext.SaveChanges();
    }
}

