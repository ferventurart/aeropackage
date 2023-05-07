using System;
using AeroPackage.Contracts.Service;
using AeroPackage.WebApp.Model.Service;
using AeroPackage.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AeroPackage.WebApp.Model;

namespace AeroPackage.WebApp.Services;

public class CompanyServicesService : ICompanyServicesService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    public CompanyServicesService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<PaginatedResult<ServiceResponse>> GetAllServices(int pageSize = 10, int pageNumber = 1, int? status = null)
    {
        var response = await _httpClient.GetAsync($"api/services?pageSize={pageSize}&pageNumber={pageNumber}&status={status}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var services = JsonSerializer.Deserialize<PaginatedResult<ServiceResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (services is not null)
            return services;
        return new PaginatedResult<ServiceResponse>();
    }


    public async Task<List<ServiceResponse>> GetServicesActiveByName(string name)
    {
        var response = await _httpClient.GetAsync($"api/services/active/{name}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var services = JsonSerializer.Deserialize<List<ServiceResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (services is not null)
            return services;
        return new List<ServiceResponse>();
    }

    public async Task<ServiceResponse> Create(CreateServiceDto service)
    {
        var response = await _httpClient.PostAsJsonAsync("api/services", service);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var serviceResponse = JsonSerializer.Deserialize<ServiceResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (serviceResponse is not null)
            return serviceResponse;

        return new ServiceResponse(string.Empty, string.Empty, 0, 0);
    }

    public async Task<ServiceResponse> Update(UpdateServiceDto service)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/services/{service.Id}", service);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var serviceResponse = JsonSerializer.Deserialize<ServiceResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (serviceResponse is not null)
            return serviceResponse;

        return new ServiceResponse(string.Empty, string.Empty, 0, 0);
    }

    public async Task<ServiceResponse> Delete(Guid Id)
    {
        var response = await _httpClient.DeleteAsync($"api/services/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var serviceResponse = JsonSerializer.Deserialize<ServiceResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (serviceResponse is not null)
            return serviceResponse;

        return new ServiceResponse(string.Empty, string.Empty, 0, 0);
    }

    public async Task<ServiceResponse> GetById(Guid Id)
    {
        var response = await _httpClient.GetAsync($"api/services/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.NotFound)
            return new ServiceResponse(string.Empty, string.Empty, 0, 0);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var service = JsonSerializer.Deserialize<ServiceResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (service is not null)
            return service;

        return new ServiceResponse(string.Empty, string.Empty, 0, 0);
    }
}

