using System;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Commands.UpdatePackageStatus;

public record UpdatePackageStatusCommand(string OwnTrackingNumber, string Status) : IRequest<ErrorOr<bool>>;


