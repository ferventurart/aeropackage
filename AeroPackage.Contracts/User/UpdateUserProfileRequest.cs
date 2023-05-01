using System;
namespace AeroPackage.Contracts.User;

public record UpdateUserProfileRequest(Guid Id, string FirstName, string LastName, string? Password, string? ConfirmPassword);
