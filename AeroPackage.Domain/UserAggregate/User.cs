using System.Linq.Expressions;
using System.Reflection;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.UserAggregate.Enums;
using AeroPackage.Domain.UserAggregate.ValueObjects;

namespace AeroPackage.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; } // TODO: Hash this
    public UserRole Role { get; }
    public UserStatus Status { get;}
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private User(string firstName, string lastName, string email, string password, UserRole role, UserStatus status, UserId? userId = null)
        : base(userId ?? UserId.CreateUnique())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
        Status = status;
    }

    public static User Create(string firstName, string lastName, string email, string password, UserRole role)
    {
        // TODO: enforce invariants
        return new User(
            firstName,
            lastName,
            email,
            password,
            role,
            UserStatus.Active);
    }


    public void UpdateProperty<T>(Expression<Func<User, T>> propertyExpression, T newValue)
    {
        var memberExpression = propertyExpression.Body as MemberExpression;
        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo != null && propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(this, newValue);
        }
    }

    public string GetUserFullName() => $"{FirstName} {LastName}";
    #pragma warning disable CS8618
    private User()
    {
    }
    #pragma warning restore CS8618
}