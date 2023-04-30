using System;
using AeroPackage.Contracts.Customer;
using AeroPackage.WebApp.Model;
using AeroPackage.WebApp.Model.Customer;

namespace AeroPackage.WebApp.Services.Interfaces;

public interface ICustomerService
{
    Task<PaginatedResult<CustomerResponse>> GetAllCustomers(int pageSize = 10, int pageNumber = 1, int? status = null);
    Task<List<CustomerResponse>> GetCustomersActiveByName(string name);
    Task<CustomerResponse> GetById(Guid Id);
    Task<CustomerResponse> Create(CreateCustomerDto customer);
    Task<CustomerResponse> Update(UpdateCustomerDto customer);
    Task<CustomerResponse> Delete(Guid Id);
}

