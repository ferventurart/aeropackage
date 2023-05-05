using System;
using AeroPackage.Domain.ServicesAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Commands.CreateService;

public record CreateServiceCommand(
    string Name,
    decimal Rate) : IRequest<ErrorOr<Service>>;

