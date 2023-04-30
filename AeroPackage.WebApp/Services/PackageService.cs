﻿using System;
using AeroPackage.Contracts.Customer;
using System.Net;
using System.Text.Json;
using AeroPackage.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using AeroPackage.Contracts.Package;
using AeroPackage.WebApp.Model.Package;
using System.Net.Http.Json;
using AeroPackage.WebApp.Model;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using AeroPackage.WebApp.Providers.Interfaces;
using System.Net.NetworkInformation;

namespace AeroPackage.WebApp.Services;

public class PackageService : IPackageService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly IBrowserFileProvider _browserFileProvider;

    public PackageService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, IBrowserFileProvider browserFileProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _browserFileProvider = browserFileProvider;
    }

    public async Task<PackageResponse> Create(CreatePackageDto package)
    {
        try
        {
            using var content = new MultipartFormDataContent();

            // Agregamos cada archivo al objeto FormData personalizado
            foreach (var file in package.Attachments)
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "Attachments",
                    FileName = file.Name
                };

                content.Add(fileContent, "Attachments", file.Name);
            }

            content.Add(new StringContent(package.UserId.ToString()), "UserId");
            content.Add(new StringContent(package.CustomerId.ToString()), "CustomerId");
            content.Add(new StringContent(package.Store), "Store");
            content.Add(new StringContent(package.Courier), "Courier");
            content.Add(new StringContent(package.CourierTrackingNumber), "CourierTrackingNumber");
            content.Add(new StringContent(package.Weight.ToString()), "Weight");
            content.Add(new StringContent(package.Description), "Description");
            content.Add(new StringContent(package.DeclaredValue.ToString()), "DeclaredValue");
            content.Add(new StringContent(package.TaxValue.ToString()), "TaxValue");

            var response = await _httpClient.PostAsync("api/packages", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {responseContent}");

            var packageResponse = JsonSerializer.Deserialize<PackageResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return packageResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public async Task<PackageResponse> Delete(int Id)
    {
        var response = await _httpClient.DeleteAsync($"api/packages/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var packageResponse = JsonSerializer.Deserialize<PackageResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return packageResponse;
    }

    public async Task<PackageResponse> GetById(int Id)
    {
        var response = await _httpClient.GetAsync($"api/packages/{Id}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var package = JsonSerializer.Deserialize<PackageResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return package;
    }

    public async Task<PackageResponse> GetByTrackingNumber(string trackingNumber)
    {
        var response = await _httpClient.GetAsync($"api/packages/{trackingNumber}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var package = JsonSerializer.Deserialize<PackageResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return package;
    }

    public async Task<CourierResponse> GetCourierByTrackingNumber(string trackinNumber)
    {
        var response = await _httpClient.GetAsync($"api/packages/couriers/{trackinNumber}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var courier = JsonSerializer.Deserialize<CourierResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return courier;
    }

    public async Task<List<DragPackageResponse>> GetPackagesByPeriodAndStore(DateTime from, DateTime to, string stores)
    {
        var response = await _httpClient.GetAsync($"api/packages/periods/stores/?from={from.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}&to={to.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}&stores={stores}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var packages = JsonSerializer.Deserialize<List<DragPackageResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return packages;
    }

    public async Task<PaginatedResult<PackageResponse>> GetPackagesByPeriodStatusAndStore(DateTime from, DateTime to, string status, string stores, int pageSize = 10, int pageNumber = 1)
    {
        var response = await _httpClient.GetAsync($"api/packages?from={from.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}&to={to.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}&status={status}&stores={stores}&pageSize={pageSize}&pageNumber={pageNumber}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var packages = JsonSerializer.Deserialize<PaginatedResult<PackageResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return packages;
    }

    public async Task<bool> UpdateStatus(UpdateStatusPackageDto package)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/packages/update-status/{package.OwnTrackingNumber}", package);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Reason: {response.ReasonPhrase}, Message: {content}");

        var updated = JsonSerializer.Deserialize<bool>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return updated;
    }
}

