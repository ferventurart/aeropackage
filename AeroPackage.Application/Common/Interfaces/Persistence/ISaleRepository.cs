using System;
using AeroPackage.Domain.SaleAggregate;

namespace AeroPackage.Application.Common.Interfaces.Persistence;

public interface ISaleRepository
{
    Task<int> GetLastInsertedId();

    void Add(Sale sale);
}

