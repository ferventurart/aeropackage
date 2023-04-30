using System;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string CellPhoneNumber,
    DateOnly BirthDate,
    int Gender,
    string Identification,
    string Address) : IRequest<ErrorOr<Customer>>;

