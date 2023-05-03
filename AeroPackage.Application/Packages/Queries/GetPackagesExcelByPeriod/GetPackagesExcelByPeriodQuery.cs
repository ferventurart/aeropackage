using System;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetPackagesExcelByPeriod;

public record GetPackagesExcelByPeriodQuery(DateTime From, DateTime To, string Status, int PageSize, int PageNumber) : IRequest<ErrorOr<byte[]>>;

