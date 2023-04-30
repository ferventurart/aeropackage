using System;
namespace AeroPackage.Contracts.User;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role);

