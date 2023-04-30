using System;
namespace AeroPackage.Contracts.User;

public record UserResponse(string Id, string FirstName, string LastName, string Email, int Role, int Status);

