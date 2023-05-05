using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;
using AeroPackage.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AeroPackageDbContext _dbContext;

    public ServiceRepository(AeroPackageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Service service)
    {
        _dbContext.Add(service);
        _dbContext.SaveChanges();
    }

    public void Update(Service service)
    {
        _dbContext.Update(service);
        _dbContext.SaveChanges();
    }

    public Service? GetServiceById(ServiceId serviceId) =>
        _dbContext.Services
                  .FirstOrDefault(f => f.Id == serviceId);


    public bool Exists(ServiceId serviceId) =>
        _dbContext
            .Services.Any(service => service.Id == serviceId);


    public void Delete(Service service)
    {
        _dbContext.Remove(service);
        _dbContext.SaveChanges();
    }

    public async Task<PaginatedResult<Service>> GetAll(int pageSize, int pageNumber) =>
         await _dbContext.Services
                         .ToPaginatedListAsync(pageNumber, pageSize);


    public async Task<PaginatedResult<Service>> GetAll(int pageSize, int pageNumber, ServiceStatus? status)
    {
        if (status is not null)
        {
            return await _dbContext.Services.Where(w => w.Status == status)
                  .ToPaginatedListAsync(pageNumber, pageSize);
        }
        return await _dbContext.Services
              .ToPaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<List<Service>> GetServicesActiveByName(string name)
    {
        return await _dbContext.Services
            .AsNoTracking()
            .Where(w => w.Status == ServiceStatus.Active && w.Name.Contains(name))
            .ToListAsync();
    }
}
