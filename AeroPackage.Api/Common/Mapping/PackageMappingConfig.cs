using AeroPackage.Application.Packages.Commands.CreatePackage;
using AeroPackage.Contracts.Customer;
using AeroPackage.Contracts.Package;
using AeroPackage.Domain.PackageAggregate;
using AeroPackage.Domain.PackageAggregate.Entities;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using Mapster;
using System;

namespace AeroPackage.Api.Common.Mapping;

public class PackageMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePackageRequest, CreatePackageCommand>();
       
        config.NewConfig<Package, PackageResponse>()
              .Map(dest => dest.Id, src => src.Id.Value.ToString())
              .Map(dest => dest.OwnTrackingNumber, src => src.OwnTrackingNumber.Value)
              .Map(dest => dest.UserId, src => src.UserId.Value)
              .Map(dest => dest.CustomerId, src => src.CustomerId.Value)
              .Map(dest => dest.Status, src => src.Status.Value);

        config.NewConfig<Courier, CourierResponse>()
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.UrlLogo, src => src.UrlLogo);
    }
}

