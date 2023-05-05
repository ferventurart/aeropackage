using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Services.Queries.GetServices;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Queries.GetServicesActiveByName;

public class GetServicesActiveByNameQueryHandler : IRequestHandler<GetServicesActiveByNameQuery, ErrorOr<List<Service>>>
{
    private readonly IServiceRepository _serviceRepository;

    public GetServicesActiveByNameQueryHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ErrorOr<List<Service>>> Handle(GetServicesActiveByNameQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return await _serviceRepository.GetServicesActiveByName(query.Name);
    }
}

