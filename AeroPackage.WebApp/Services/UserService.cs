using System;
using AeroPackage.Contracts.User;
using AeroPackage.WebApp.Model.User;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AeroPackage.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using AeroPackage.WebApp.Model;

namespace AeroPackage.WebApp.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    public UserService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<PaginatedResult<UserResponse>> GetAllUsers(int pageSize = 10, int pageNumber = 1)
    {
        var response = await _httpClient.GetAsync($"api/users?pageSize={pageSize}&pageNumber={pageNumber}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var users = JsonSerializer.Deserialize<PaginatedResult<UserResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return users;
    }

    public async Task<UserResponse> Create(CreateUserDto user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", user);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var userResponse = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return userResponse;
    }

    public async Task<UserResponse> Update(UpdateUserDto user)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}", user);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var userResponse = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return userResponse;
    }

    public async Task<UserResponse> Delete(Guid Id)
    {
        var response = await _httpClient.DeleteAsync($"api/users/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var userResponse = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return userResponse;
    }

    public async Task<UserResponse> GetById(Guid Id)
    {
        var response = await _httpClient.GetAsync($"api/users/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var user = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return user;
    }

    public async Task<UserResponse> UpdateProfile(UpdateUserProfileDto user)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}/profile", user);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var userResponse = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return userResponse;
    }
}

