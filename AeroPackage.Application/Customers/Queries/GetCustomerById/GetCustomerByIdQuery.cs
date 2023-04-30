using System;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<ErrorOr<Customer>>;

