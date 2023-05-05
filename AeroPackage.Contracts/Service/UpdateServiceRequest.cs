using System;
namespace AeroPackage.Contracts.Service;

public record UpdateServiceRequest(
    Guid Id,
    string Name,
    decimal Rate,
    int Status);

