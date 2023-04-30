using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodStatusAndStore;

public record GetPackagesByPeriodStatusAndStoreQuery(DateTime from, DateTime to, string status, string stores, int pageSize, int pageNumber) : IRequest<ErrorOr<PaginatedResult<Package>>>;