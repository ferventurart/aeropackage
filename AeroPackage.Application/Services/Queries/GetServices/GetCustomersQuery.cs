using System;
using AeroPackage.Application.Common.Models;
using AeroPackage.Domain.ServicesAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Services.Queries.GetServices;

public record GetServicesQuery(int PageSize, int PageNumber, int? Status) : IRequest<ErrorOr<PaginatedResult<Service>>>;