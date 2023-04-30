using System;
using AeroPackage.Contracts.Authentication;
using AeroPackage.WebApp.Model.Authentication;

namespace AeroPackage.WebApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginDto model);
        Task Logout();
    }
}

