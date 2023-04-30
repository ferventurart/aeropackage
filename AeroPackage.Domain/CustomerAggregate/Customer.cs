using System;
using System.Linq.Expressions;
using System.Reflection;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.Common.ValueObjects;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;

namespace AeroPackage.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId, Guid>
{
    private readonly List<PackageId> packageIds = new();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public PhoneNumber CellPhoneNumber { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public NationalIdentification Identification { get; private set; }
    public string Address { get; private set; }
    public CustomerStatus Status { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public IReadOnlyList<PackageId> PackageIds => packageIds.AsReadOnly();

    private Customer(
        CustomerId customerId,
        string firstName,
        string lastName,
        string email,
        PhoneNumber cellPhoneNumber,
        DateOnly birthDate,
        Gender gender,
        NationalIdentification identification,
        string address,
        CustomerStatus status) : base(customerId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CellPhoneNumber = cellPhoneNumber;
        BirthDate = birthDate;
        Gender = gender;
        Identification = identification;
        Address = address;
        Status = status;
    }

    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        PhoneNumber cellPhoneNumber,
        DateOnly birthDate,
        Gender gender,
        NationalIdentification identification,
        string address)
    {
        return new Customer(
            CustomerId.CreateUnique(),
            firstName,
            lastName,
            email,
            cellPhoneNumber,
            birthDate,
            gender,
            identification,
            address,
            CustomerStatus.Active);
    }

    public void UpdateProperty<T>(Expression<Func<Customer, T>> propertyExpression, T newValue)
    {
        var memberExpression = propertyExpression.Body as MemberExpression;
        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo != null && propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(this, newValue);
        }
    }

    public void AssignPackageToCustomer(PackageId packageId) => packageIds.Add(packageId);
    public void RemovePackagetFromCustomer(PackageId packageId) => packageIds.Remove(packageId);
    public string GetCustomerFullName() => $"{FirstName} {LastName}";

    #pragma warning disable CS8618
    private Customer()
    {
    }
    #pragma warning restore CS8618
}

