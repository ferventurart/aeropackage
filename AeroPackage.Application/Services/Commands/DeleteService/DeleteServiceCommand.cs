using System;
using AeroPackage.Domain.ServicesAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Commands.DeleteService;

public record DeleteServiceCommand(Guid Id) : IRequest<ErrorOr<Service>>;

