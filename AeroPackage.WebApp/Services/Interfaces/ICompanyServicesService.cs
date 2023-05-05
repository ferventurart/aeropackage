using System;
using AeroPackage.Contracts.Service;
using AeroPackage.WebApp.Model;
using AeroPackage.WebApp.Model.Service;

namespace AeroPackage.WebApp.Services.Interfaces;

public interface ICompanyServicesService
{
    Task<PaginatedResult<ServiceResponse>> GetAllServices(int pageSize = 10, int pageNumber = 1, int? status = null);
    Task<List<ServiceResponse>> GetServicesActiveByName(string name);
    Task<ServiceResponse> GetById(Guid Id);
    Task<ServiceResponse> Create(CreateServiceDto customer);
    Task<ServiceResponse> Update(UpdateServiceDto customer);
    Task<ServiceResponse> Delete(Guid Id);
}

