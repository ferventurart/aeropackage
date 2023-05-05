using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;

namespace AeroPackage.Application.Common.Interfaces.Persistence;

public interface IServiceRepository
{
    bool Exists(ServiceId serviceId);
    Service? GetServiceById(ServiceId serviceId);
    Task<PaginatedResult<Service>> GetAll(int pageSize, int pageNumber, ServiceStatus? status);
    Task<List<Service>> GetServicesActiveByName(string name);
    void Add(Service service);
    void Update(Service service);
    void Delete(Service service);
}

