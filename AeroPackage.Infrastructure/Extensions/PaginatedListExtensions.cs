using System;
using AeroPackage.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace AeroPackage.Infrastructure.Extensions;

public static class PaginatedListExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedResult<T>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = count,
            Results = items
        };
    }
}

