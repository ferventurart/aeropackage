using System;
using AeroPackage.Application.Sales.Commands.CreateInvoice;
using AeroPackage.Contracts.Package;
using AeroPackage.Contracts.Sale;
using AeroPackage.Domain.SaleAggregate;
using Mapster;

namespace AeroPackage.Api.Common.Mapping;

public class SaleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateInvoiceRequest, CreateInvoiceCommand>();
        config.NewConfig<InvoiceDetailRequest, InvoiceItemCommand>();

        config.NewConfig<Sale, SaleResponse>()
              .Map(dest => dest.Id, src => src.Id.Value.ToString())
              .Map(dest => dest.InvoiceNumber, src => src.InvoiceNumber.Value)
              .Map(dest => dest.UserId, src => src.UserId.Value)
              .Map(dest => dest.CustomerId, src => src.CustomerId.Value)
              .Map(dest => dest.Status, src => src.Status.Value);
    }
}

