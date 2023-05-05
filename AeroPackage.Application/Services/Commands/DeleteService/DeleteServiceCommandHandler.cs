using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Services.Commands.CreateService;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Services.Commands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ErrorOr<Service>>
{
    private readonly IServiceRepository _serviceRepository;

    public DeleteServiceCommandHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ErrorOr<Service>> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var service = _serviceRepository.GetServiceById(ServiceId.Create(command.Id));

        if(service is null)
        {
            return Errors.Service.NotFound;
        }

        _serviceRepository.Delete(service);

        return service;
    }
}

