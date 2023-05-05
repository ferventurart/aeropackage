using System;
using AeroPackage.Domain.ServicesAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Queries.GetServiceById;

public record GetServiceByIdQuery(Guid Id) : IRequest<ErrorOr<Service>>;

