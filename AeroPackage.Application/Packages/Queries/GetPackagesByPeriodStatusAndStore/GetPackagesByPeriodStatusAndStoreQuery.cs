using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodStatusAndStore;

public record GetPackagesByPeriodStatusAndStoreQuery(DateTime From, DateTime To, string Status, string Stores, int PageSize, int PageNumber) : IRequest<ErrorOr<PaginatedResult<Package>>>;