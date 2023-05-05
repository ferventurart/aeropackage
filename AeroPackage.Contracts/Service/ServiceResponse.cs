using System;
namespace AeroPackage.Contracts.Service;

public record ServiceResponse(
 string Id,
 string Name,
 decimal Rate,
 int Status);

