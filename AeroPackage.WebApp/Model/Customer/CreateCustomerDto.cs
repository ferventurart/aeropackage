using System;
namespace AeroPackage.WebApp.Model.Customer;

public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CellPhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Gender { get; set; }
    public string Identification { get; set; }
    public string Address { get; set; }
}

