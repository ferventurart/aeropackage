using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Contracts.Package;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesByPeriodAndStore;

public record GetPackagesByPeriodAndStoreQuery(DateTime From, DateTime To, string Stores) : IRequest<ErrorOr<List<DragPackageResponse>>>;