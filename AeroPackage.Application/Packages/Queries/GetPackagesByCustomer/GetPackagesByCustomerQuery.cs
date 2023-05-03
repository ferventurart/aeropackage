using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByCustomer;

public record GetPackageByCustomerQuery(Guid Id, DateTime From, DateTime To, int PageSize, int PageNumber) : IRequest<ErrorOr<PaginatedResult<Package>>>;
