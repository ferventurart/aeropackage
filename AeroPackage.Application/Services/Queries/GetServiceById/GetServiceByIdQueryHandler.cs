using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Services.Commands.CreateService;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;
using ErrorOr;
using MediatR;


namespace AeroPackage.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ErrorOr<Service>>
{
    private readonly IServiceRepository _serviceRepository;

    public GetServiceByIdQueryHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ErrorOr<Service>> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var service = _serviceRepository.GetServiceById(ServiceId.Create(query.Id));

        if (service is null)
        {
            return Errors.Service.NotFound;
        }

        return service;
    }
}

