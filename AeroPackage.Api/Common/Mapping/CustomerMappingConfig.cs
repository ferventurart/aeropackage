using AeroPackage.Application.Customers.Commands.CreateCustomer;
using AeroPackage.Application.Customers.Commands.DeleteCustomer;
using AeroPackage.Application.Customers.Commands.UpdateCustomer;
using AeroPackage.Contracts.Customer;
using AeroPackage.Domain.CustomerAggregate;
using Mapster;

namespace AeroPackage.Api.Common.Mapping;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCustomerRequest, CreateCustomerCommand>();
        config.NewConfig<UpdateCustomerRequest, UpdateCustomerCommand>();
        config.NewConfig<DeleteCustomerRequest, DeleteCustomerCommand>();

        config.NewConfig<Customer, CustomerResponse>()
              .Map(dest => dest.Id, src => src.Id.Value.ToString())
              .Map(dest => dest.CellPhoneNumber, src => src.CellPhoneNumber.Value)
              .Map(dest => dest.Gender, src => src.Gender.Value)
              .Map(dest => dest.Identification, src => src.Identification.Value)
              .Map(dest => dest.Status, src => src.Status.Value);
    }
}