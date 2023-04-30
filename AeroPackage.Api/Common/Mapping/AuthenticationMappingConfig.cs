using AeroPackage.Application.Authentication.Commands.Register;
using AeroPackage.Application.Authentication.Common;
using AeroPackage.Application.Authentication.Queries.Login;
using AeroPackage.Contracts.Authentication;

using Mapster;

namespace AeroPackage.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value.ToString())
            .Map(dest => dest, src => src.User);
    }
}