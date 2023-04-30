using System;
using AeroPackage.Contracts.Authentication;
using System.Net.Http.Json;
using AeroPackage.Contracts.Customer;
using AeroPackage.WebApp.Services.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using AeroPackage.WebApp.Model.Customer;
using System.Text.Json;
using AeroPackage.WebApp.Model;
using System.Net.NetworkInformation;

namespace AeroPackage.WebApp.Services;

public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    public CustomerService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<PaginatedResult<CustomerResponse>> GetAllCustomers(int pageSize = 10, int pageNumber = 1, int? status = null)
    {
        var response = await _httpClient.GetAsync($"api/customers?pageSize={pageSize}&pageNumber={pageNumber}&status={status}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var customers = JsonSerializer.Deserialize<PaginatedResult<CustomerResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return customers;
    }


    public async Task<List<CustomerResponse>> GetCustomersActiveByName(string name)
    {
        var response = await _httpClient.GetAsync($"api/customers/active/{name}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var customers = JsonSerializer.Deserialize<List<CustomerResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return customers;
    }

    public async Task<CustomerResponse> Create(CreateCustomerDto customer)
    {
        var response = await _httpClient.PostAsJsonAsync("api/customers", customer);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var customerResponse = JsonSerializer.Deserialize<CustomerResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return customerResponse;
    }

    public async Task<CustomerResponse> Update(UpdateCustomerDto customer)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/customers/{customer.Id}", customer);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var customerResponse = JsonSerializer.Deserialize<CustomerResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return customerResponse;
    }

    public async Task<CustomerResponse> Delete(Guid Id)
    {
        var response = await _httpClient.DeleteAsync($"api/customers/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var customerResponse = JsonSerializer.Deserialize<CustomerResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return customerResponse;
    }

    public async Task<CustomerResponse> GetById(Guid Id)
    {
        var response = await _httpClient.GetAsync($"api/customers/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var customer = JsonSerializer.Deserialize<CustomerResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return customer;
    }
}

