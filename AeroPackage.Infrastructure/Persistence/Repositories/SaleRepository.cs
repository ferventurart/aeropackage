using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.SaleAggregate;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AeroPackageDbContext _dbContext;

    public SaleRepository(AeroPackageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Sale sale)
    {
        _dbContext.Add(sale);
        _dbContext.SaveChanges();
    }

    public async Task<int> GetLastInsertedId()
    {
        var lastInserted = await _dbContext.Sales.OrderByDescending(n => n.Id).FirstOrDefaultAsync();
        int nextNumber = 1;
        if (lastInserted is not null)
        {
            nextNumber = lastInserted.Id.Value + 1;
        }
        return nextNumber;
    }
}

