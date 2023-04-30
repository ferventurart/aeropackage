using System;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using AeroPackage.WebApp.Utility;
using Microsoft.AspNetCore.Components;
using System.Net;
using AeroPackage.Contracts.Authentication;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using AeroPackage.WebApp.Providers.Interfaces;

namespace AeroPackage.WebApp.Providers;

public class AuthProvider : AuthenticationStateProvider, IAuthProvider
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;
    public AuthProvider(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (string.IsNullOrEmpty(token))
            {
                return _anonymous;
            }

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _http.PostAsJsonAsync($"auth/token/verify", "");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                NotifyUserLogout();
            }

            var authResult = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{authResult.FirstName} {authResult.LastName}"),
                new Claim(ClaimTypes.Email, authResult.Email),
                new Claim(ClaimTypes.Role, GetRoleFromJWT(token)),
            };
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType")));

        }
        catch (Exception ex)
        {
            NotifyUserLogout();
            return _anonymous;
        }

    }

    public void NotifyUserAuthentication(string token)
    {
        var authUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authUser));
        NotifyAuthenticationStateChanged(authState);

    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }

    public string GetRoleFromJWT(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        var roleClaim = token.Claims.FirstOrDefault(c => c.Type == "role");
        if (roleClaim != null)
        {
            return roleClaim.Value;
        }
        else
        {
            return null;
        }
    }

    public async Task<Guid> GetCurrentUserId()
    {
        var jwt = await _localStorage.GetItemAsync<string>("jwt");
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        var idClaim = token.Claims.FirstOrDefault(c => c.Type == "id");
        if (idClaim != null)
        {
            return Guid.Parse(idClaim.Value);
        }
        else
        {
            return Guid.Empty;
        }
    }
}


