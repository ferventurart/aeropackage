using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Services.Commands.CreateService;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;
using AeroPackage.Domain.Common.DomainErrors;
using ErrorOr;
using MediatR;
using System;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.ServicesAggregate.Enums;

namespace AeroPackage.Application.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ErrorOr<Service>>
{
    private readonly IServiceRepository _serviceRepository;

    public UpdateServiceCommandHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ErrorOr<Service>> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_serviceRepository.GetServiceById(ServiceId.Create(command.Id)) is not Service service)
        {
            return Errors.Service.NotFound;
        }

        service.UpdateProperty(c => c.Name, command.Name);
        service.UpdateProperty(c => c.Rate, command.Rate);
        service.UpdateProperty(c => c.Status, ServiceStatus.FromValue(command.Status));
        service.UpdateProperty(c => c.UpdatedDateTime, DateTime.Now);

        _serviceRepository.Update(service);

        return service;
    }
}

