using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AeroPackageDbContext _dbContext;

    public CustomerRepository(AeroPackageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Customer customer)
    {
        _dbContext.Add(customer);
        _dbContext.SaveChanges();
    }

    public void Update(Customer customer)
    {
        _dbContext.Update(customer);
        _dbContext.SaveChanges();
    }

    public Customer? GetCustomerByEmail(string email) =>
         _dbContext.Customers
                   .FirstOrDefault(f => f.Email == email);


    public Customer? GetCustomerById(CustomerId customerId) =>
        _dbContext.Customers
                  .FirstOrDefault(f => f.Id == customerId);

    public Customer? GetCustomerByIdentification(NationalIdentification identification) =>
        _dbContext.Customers
                  .FirstOrDefault(customer => customer.Identification == identification);


    public bool Exists(CustomerId customerId) =>
        _dbContext
            .Customers.Any(customer => customer.Id == customerId);


    public void Delete(Customer customer)
    {
        _dbContext.Remove(customer);
        _dbContext.SaveChanges();
    }

    public async Task<PaginatedResult<Customer>> GetAll(int pageSize, int pageNumber) =>
         await _dbContext.Customers
                         .ToPaginatedListAsync(pageNumber, pageSize);
    

    public async Task<PaginatedResult<Customer>> GetAll(int pageSize, int pageNumber, CustomerStatus? status)
    {
        if (status is not null)
        {
            return await _dbContext.Customers.Where(w => w.Status == status)
                  .ToPaginatedListAsync(pageNumber, pageSize);
        }
        return await _dbContext.Customers
              .ToPaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<List<Customer>> GetCustomersActiveByName(string name)
    {
        return await _dbContext.Customers
            .AsNoTracking()
            .Where(w => w.Status == CustomerStatus.Active && (w.FirstName + " " + w.LastName).Contains(name))
            .ToListAsync();
    }
}

