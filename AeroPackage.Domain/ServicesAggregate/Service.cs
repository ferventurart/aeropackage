using System;
using System.Linq.Expressions;
using System.Reflection;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.CustomerAggregate.Enums;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.ServicesAggregate.Enums;
using AeroPackage.Domain.ServicesAggregate.ValueObjects;

namespace AeroPackage.Domain.ServicesAggregate;

public sealed class Service : AggregateRoot<ServiceId, Guid>
{
    public string Name { get; private set; }
    public decimal Rate { get; private set; }
    public ServiceStatus Status { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private Service(
        string name,
        decimal rate,
        ServiceStatus status)
    {
        Name = name;
        Rate = rate;
        Status = status;
    }

    public static Service Create(string name, decimal rate)
    {
        return new Service(name, rate, ServiceStatus.Active);
    }

    public void UpdateProperty<T>(Expression<Func<Service, T>> propertyExpression, T newValue)
    {
        var memberExpression = propertyExpression.Body as MemberExpression;
        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo != null && propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(this, newValue);
        }
    }

    #pragma warning disable CS8618
    private Service()
    {
    }
    #pragma warning restore CS8618
}

