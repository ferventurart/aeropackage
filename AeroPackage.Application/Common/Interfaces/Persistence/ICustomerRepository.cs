using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;

namespace AeroPackage.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    bool Exists(CustomerId customerId);
    Customer? GetCustomerById(CustomerId customerId);
    Customer? GetCustomerByEmail(string email);
    Customer? GetCustomerByIdentification(NationalIdentification identification);
    Task<PaginatedResult<Customer>> GetAll(int pageSize, int pageNumber, CustomerStatus? status);
    Task<List<Customer>> GetCustomersActiveByName(string name);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}

