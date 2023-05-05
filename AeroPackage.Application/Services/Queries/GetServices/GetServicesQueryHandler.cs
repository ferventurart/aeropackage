using System;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Models;
using AeroPackage.Application.Services.Queries.GetServiceById;
using AeroPackage.Domain.ServicesAggregate;
using AeroPackage.Domain.ServicesAggregate.Enums;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, ErrorOr<PaginatedResult<Service>>>
{
    private readonly IServiceRepository _serviceRepository;

    public GetServicesQueryHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ErrorOr<PaginatedResult<Service>>> Handle(GetServicesQuery query, CancellationToken cancellationToken)
    {
        return await _serviceRepository.GetAll(query.PageSize, query.PageNumber, query.Status is not null ? ServiceStatus.FromValue(query.Status.Value) : null);
    }
}

