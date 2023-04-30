using System;
using AeroPackage.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Customers.Queries.GetCustomerByIdentification;

public record GetCustomerByIdentificationQuery(string Identification) : IRequest<ErrorOr<Customer>>;

