using System;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string CellPhoneNumber,
    DateOnly BirthDate,
    int Gender,
    string Identification,
    string Address,
    int Status) : IRequest<ErrorOr<Customer>>;


