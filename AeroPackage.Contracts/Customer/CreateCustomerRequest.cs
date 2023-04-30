using System;
namespace AeroPackage.Contracts.Customer;

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string CellPhoneNumber,
    DateOnly BirthDate,
    int Gender,
    string Identification,
    string Address);

