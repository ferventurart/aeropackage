using System;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<Customer>>;

