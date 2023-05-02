using System;
using System.Net.Http.Headers;
using AeroPackage.Contracts.Customer;
using AeroPackage.Contracts.Package;
using AeroPackage.WebApp.Model;
using AeroPackage.WebApp.Model.Common;
using AeroPackage.WebApp.Model.Customer;
using AeroPackage.WebApp.Model.Package;
using Microsoft.JSInterop;

namespace AeroPackage.WebApp.Services.Interfaces;

public interface IPackageService
{
    Task<PaginatedResult<PackageResponse>> GetPackagesByPeriodStatusAndStore(DateTime from, DateTime to, string status, string stores, int pageSize = 10, int pageNumber = 1);
    Task<List<DragPackageResponse>> GetPackagesByPeriodAndStore(DateTime from, DateTime to, string stores);
    Task<CourierResponse> GetCourierByTrackingNumber(string trackingNumber);
    Task<PackageResponse> GetById(int Id);
    Task<PackageResponse> GetByTrackingNumber(string trackingNumber);
    Task<PackageResponse> Create(CreatePackageDto package);
    Task<PackageResponse> Update(UpdatePackageDto package);
    Task<bool> UpdateStatus(UpdateStatusPackageDto package);
    Task<PackageResponse> Delete(int Id);
    Task<PackageResponse> DeleteAttachment(int Id, DeleteAttachmentDto attachment);
}

