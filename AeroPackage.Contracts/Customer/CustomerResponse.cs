using System;
namespace AeroPackage.Contracts.Customer;

public record CustomerResponse(
 string Id,
 string FirstName,
 string LastName,
 string Email,
 string CellPhoneNumber,
 DateOnly BirthDate,
 int Gender,
 string Identification,
 string Address,
 int Status);

