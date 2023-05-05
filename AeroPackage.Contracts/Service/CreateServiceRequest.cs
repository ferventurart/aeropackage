using System;
namespace AeroPackage.Contracts.Service;

public record CreateServiceRequest(
    string Name,
    decimal Rate);

