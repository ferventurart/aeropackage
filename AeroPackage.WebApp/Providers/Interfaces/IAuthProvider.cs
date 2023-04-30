using System;
namespace AeroPackage.WebApp.Providers.Interfaces;

public interface IAuthProvider
{
    void NotifyUserAuthentication(string token);
    void NotifyUserLogout();
    string GetRoleFromJWT(string jwt);
    Task<Guid> GetCurrentUserId();
}

