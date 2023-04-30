using System;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Queries.GetCourierByTrackingNumber;

public record GetCourierByTrcknNumberQuery(string TrackingNumber) : IRequest<ErrorOr<Courier?>>;


