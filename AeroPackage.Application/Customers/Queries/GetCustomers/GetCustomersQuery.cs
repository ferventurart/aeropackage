using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Queries.GetCustomers;

public record GetCustomersQuery(int PageSize, int PageNumber, int? Status) : IRequest<ErrorOr<PaginatedResult<Customer>>>;