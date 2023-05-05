using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;

namespace AeroPackage.Application.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ErrorOr<Service>>
{
    private readonly IServiceRepository _serviceRepository;

    public CreateServiceCommandHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ErrorOr<Service>> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;


        var service = Service.Create(
            command.Name,
            command.Rate);

        service.UpdateProperty(c => c.CreatedDateTime, DateTime.Now);

        _serviceRepository.Add(service);

        return service;
    }
}

