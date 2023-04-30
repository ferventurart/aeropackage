using System;
using AeroPackage.WebApp.Model.Authentication;
using AeroPackage.WebApp.Services.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AeroPackage.Contracts.Authentication;
using AeroPackage.WebApp.Providers;

namespace AeroPackage.WebApp.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticationResponse> Login(LoginDto model)
    {

        var loginResult = await _httpClient.PostAsJsonAsync($"auth/login", new LoginRequest(model.Email, model.Password));
        if (!loginResult.IsSuccessStatusCode)
            return new AuthenticationResponse(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        var loginResponseContent = await loginResult.Content.ReadFromJsonAsync<AuthenticationResponse>();
        if (loginResponseContent is not null)
        {
            await _localStorage.SetItemAsync("jwt", loginResponseContent.Token);
            ((AuthProvider)_authStateProvider).NotifyUserAuthentication(loginResponseContent.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponseContent.Token);
        }
        return loginResponseContent;

    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("jwt");
        ((AuthProvider)_authStateProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;

    }

}
