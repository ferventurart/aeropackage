using System;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Queries.GetCustomersActiveByName;

public record GetCustomersActiveByNameQuery(string Name) : IRequest<ErrorOr<List<Customer>>>;

