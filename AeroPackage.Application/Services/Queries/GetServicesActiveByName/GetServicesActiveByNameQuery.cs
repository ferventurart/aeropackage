using System;
using AeroPackage.Domain.ServicesAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Queries.GetServicesActiveByName;

public record GetServicesActiveByNameQuery(string Name) : IRequest<ErrorOr<List<Service>>>;

