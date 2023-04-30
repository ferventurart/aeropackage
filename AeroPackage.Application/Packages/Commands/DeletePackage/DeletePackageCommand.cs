using System;
using AeroPackage.Domain.PackageAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Commands.DeletePackage;

public record DeletePackageCommand(int Id) : IRequest<ErrorOr<Package>>;


