using System;
using AeroPackage.Application.Services.Commands.CreateService;
using AeroPackage.Application.Services.Commands.DeleteService;
using AeroPackage.Application.Services.Commands.UpdateService;
using AeroPackage.Contracts.Service;
using AeroPackage.Domain.ServicesAggregate;
using Mapster;

namespace AeroPackage.Api.Common.Mapping;

public class ServiceMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateServiceRequest, CreateServiceCommand>();
        config.NewConfig<UpdateServiceRequest, UpdateServiceCommand>();
        config.NewConfig<DeleteServiceRequest, DeleteServiceCommand>();

        config.NewConfig<Service, ServiceResponse>()
              .Map(dest => dest.Id, src => src.Id.Value.ToString())
              .Map(dest => dest.Status, src => src.Status.Value);
    }
}

