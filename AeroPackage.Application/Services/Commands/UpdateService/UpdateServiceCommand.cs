using System;
using AeroPackage.Domain.ServicesAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Commands.UpdateService;

public record UpdateServiceCommand(
    Guid Id,
    string Name,
    decimal Rate,
    int Status) : IRequest<ErrorOr<Service>>;


