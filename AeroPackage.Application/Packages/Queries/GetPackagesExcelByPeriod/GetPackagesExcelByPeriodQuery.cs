using System;
using ErrorOr;
using MediatR;
using OfficeOpenXml;

namespace AeroPackage.Application.Packages.Queries.GetPackagesExcelByPeriod;

public record GetPackagesExcelByPeriodQuery(DateTime from, DateTime to, string status, int pageSize, int pageNumber) : IRequest<ErrorOr<byte[]>>;

