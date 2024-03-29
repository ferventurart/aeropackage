﻿using System;
using AeroPackage.Application.Packages.Commands.CreatePackage;
using AeroPackage.Contracts.Package;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using AeroPackage.Application.Packages.Commands.DeletePackage;
using AeroPackage.Application.Packages.Queries.GetPackagesByCustomer;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using AeroPackage.Application.Customers.Queries.GetCustomerByIdentification;
using AeroPackage.Application.Packages.Queries.GetCourierByTrackingNumber;
using AeroPackage.Application.Common.Models;
using AeroPackage.Api.Handlers.Interfaces;
using AeroPackage.Application.Customers.Queries.GetCustomerById;
using AeroPackage.Application.Packages.Queries.GetPackageByOwnTracker;
using AeroPackage.Application.Packages.Queries.GetPackagesByPeriodStatusAndStore;
using AeroPackage.Application.Packages.Queries.GetPackagesByPeriodAndStore;
using AeroPackage.Application.Packages.Commands.UpdatePackageStatus;
using AeroPackage.Application.Customers.Commands.UpdateCustomer;
using AeroPackage.Contracts.Customer;
using AeroPackage.Application.Packages.Commands.UpdatePackage;
using AeroPackage.Application.Packages.Commands.DeleteAttachment;
using AeroPackage.Application.Packages.Queries.GetPackagesExcelByPeriod;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace AeroPackage.Api.Controllers;

[Route("api/packages")]
[Authorize(Roles = "Admin,Executive,Warehouse")]
public class PackagesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    private readonly IFileHandler _fileHandler;

    public PackagesController(IMapper mapper, ISender mediator, IFileHandler fileHandler)
    {
        _mapper = mapper;
        _mediator = mediator;
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Get all Packages
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="stores"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(DateTime from, DateTime to, string status, string stores, int pageSize = 10, int pageNumber = 1)
    {
        var query = new GetPackagesByPeriodStatusAndStoreQuery(from, to, status, stores, pageSize, pageNumber);

        var getPackageResult = await _mediator.Send(query);

        return getPackageResult.Match(
            packages => Ok(_mapper.Map<PaginatedResult<PackageResponse>>(packages)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get all Packages
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="stores"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet("reports/excel/by-period")]
    public async Task<IActionResult> GetExcel(DateTime from, DateTime to, string status, int pageSize = 10, int pageNumber = 1)
    {
        var query = new GetPackagesExcelByPeriodQuery(from, to, status, pageSize, pageNumber);

        var getExcelResult = await _mediator.Send(query);

        if (getExcelResult.IsError)
        {
            return NoContent();
        }

        var stream = new MemoryStream(getExcelResult.Value);

        var fileName = $"{ from.ToString("dd_MM_yyyy") }_{ to.ToString("dd_MM_yyyy")}.xlsx";
      
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// Get all Packages
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="stores"></param>
    /// <returns></returns>
    [HttpGet("periods/stores/")]
    public async Task<IActionResult> GetByPeriodAndStore(DateTime from, DateTime to, string stores)
    {
        var query = new GetPackagesByPeriodAndStoreQuery(from, to, stores);

        var getPackageResult = await _mediator.Send(query);

        return getPackageResult.Match(
            packages => Ok(packages),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get Couries By Courier Tracking Number
    /// </summary>
    /// <param name="trackingNumber"></param>
    /// <returns></returns>
    [HttpGet("couriers/{trackingNumber}")]
    public async Task<IActionResult> GetByIdentification(string trackingNumber)
    {
        var query = new GetCourierByTrcknNumberQuery(trackingNumber);

        var getCouriersResult = await _mediator.Send(query);

        return getCouriersResult.Match(
            courier => Ok(_mapper.Map<CourierResponse>(courier)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Get Package by Internal Tracking Number
    /// </summary>
    /// <param name="trackingNumber"></param>
    /// <returns></returns>
    [HttpGet("{trackingNumber}")]
    public async Task<IActionResult> Get(string trackingNumber)
    {
        var query = new GetPackageByOwnTrackerQuery(trackingNumber);

        var getPackageResult = await _mediator.Send(query);

        return getPackageResult.Match(
            package => Ok(_mapper.Map<PackageResponse>(package)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Create Package
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreatePackageRequest request)
    {
        var temporalFolder = Guid.NewGuid().ToString();
        List<string> attachmentsUrls = new();

        if (request.Attachments is not null && request.Attachments.Count > 0)
        {
            attachmentsUrls = await _fileHandler.UploadFilesToApi(request.Attachments, temporalFolder);
        }

        var command = new CreatePackageCommand(
            request.UserId,
            request.CustomerId,
            request.Store,
            request.Courier,
            request.CourierTrackingNumber,
            request.Weight,
            request.QuantityArticles,
            request.Description,
            request.DeclaredValue,
            attachmentsUrls);

        var createPackageResult = await _mediator.Send(command);

        if (createPackageResult.IsError && attachmentsUrls.Count > 0)
        {
            _fileHandler.DeleteFolderWithPackageAttachments(temporalFolder);
        }

        if (!createPackageResult.IsError && createPackageResult.Value is not null && attachmentsUrls.Count > 0)
        {
            await _fileHandler.MoveFilesToDestinationFolder(temporalFolder, createPackageResult.Value.OwnTrackingNumber.Value);
            _fileHandler.DeleteFolderWithPackageAttachments(temporalFolder);
        }

        return createPackageResult.Match(
            package => Ok(_mapper.Map<PackageResponse>(package)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Update Package
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdatePackageRequest request)
    {
        if (id != request.Id)
        {
            List<Error> errors = new();
            errors.Add(Error.Validation("PackageId", "Url Id is not equal than Request Id."));
            return Problem(errors);
        }

        List<string> attachmentsUrls = new();

        if (request.Attachments is not null && request.Attachments.Count > 0)
        {
            attachmentsUrls = await _fileHandler.UploadFilesToApi(request.Attachments, request.OwnTrackingNumber);
        }

        var command = new UpdatePackageCommand(
                   request.Id,
                   request.UserId,
                   request.CustomerId,
                   request.Store,
                   request.Courier,
                   request.CourierTrackingNumber,
                   request.Weight,
                   request.QuantityArticles,
                   request.Description,
                   request.DeclaredValue,
                   attachmentsUrls);

        var updatePackageResult = await _mediator.Send(command);

        return updatePackageResult.Match(
            package => Ok(_mapper.Map<PackageResponse>(package)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Update Package
    /// </summary>
    /// <param name="trackingNumber"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update-status/{trackingNumber}")]
    public async Task<IActionResult> ChangeStatus(string trackingNumber, UpdateStatusPackageRequest request)
    {
        var command = _mapper.Map<UpdatePackageStatusCommand>((request));

        var updatePackageResult = await _mediator.Send(command);

        return updatePackageResult.Match(
            package => Ok(updatePackageResult.Value),
            errors => Problem(errors));
    }

    /// <summary>
    /// Delete Package
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, DeletePackageRequest request)
    {
        if (id != request.Id)
        {
            List<Error> errors = new();
            errors.Add(Error.Validation("PackageId", "Url Id is not equal than Request Id."));
            return Problem(errors);
        }

        var command = _mapper.Map<DeletePackageCommand>((request));

        var deletePackageResult = await _mediator.Send(command);

        return deletePackageResult.Match(
            package => Ok(_mapper.Map<PackageResponse>(package)),
            errors => Problem(errors));
    }

    /// <summary>
    /// Delete Package
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("{id}/attachments")]
    public async Task<IActionResult> DeleteAttachments(int id, [FromBody] DeleteAttachmentRequest request)
    {
        if (id != request.Id)
        {
            List<Error> errors = new();
            errors.Add(Error.Validation("PackageId", "Url Id is not equal than Request Id."));
            return Problem(errors);
        }

        var command = _mapper.Map<DeleteAttachmentCommand>((request));

        var deleteAttachmentResult = await _mediator.Send(command);

        if (!deleteAttachmentResult.IsError)
        {
            _fileHandler.DeleteFile(request.FileName, request.OwnTrackingNumber);
        }

        return deleteAttachmentResult.Match(
            package => Ok(_mapper.Map<PackageResponse>(package)),
            errors => Problem(errors));
    }
}

