using System;
namespace AeroPackage.Contracts.User;

public record UpdateUserRequest(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    int Role,
    int Status);

