using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.Constants;
using AeroPackage.Domain.PackageAggregate.Entities;
using AeroPackage.Domain.PackageAggregate.Enums;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;

using System;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AeroPackage.Domain.PackageAggregate;

public sealed class Package : AggregateRoot<PackageId, int>
{
    private readonly List<string> _attachments = new();
    private readonly List<PackageHistory> _packageHistories = new();

    public OwnTrackingNumber OwnTrackingNumber { get; private set; }
    public UserId UserId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public string Consignee { get; private set; }
    public string Store { get; private set; }
    public string? Courier { get; private set; }
    public string? CourierTrackingNumber { get; private set; }
    public decimal Weight { get; private set; }
    public int QuantityArticles { get; private set; }
    public string Description { get; private set; }
    public decimal DeclaredValue { get; private set; }
    public decimal TaxValue { get; private set; }
    public PackageStatus Status { get; private set; }
    public bool Paid { get; private set; }

    public IReadOnlyList<string> Attachments => _attachments.AsReadOnly();
    public IReadOnlyList<PackageHistory> PackageHistories => _packageHistories.AsReadOnly();

    public string CreatedBy { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private Package(
        PackageId packageId,
        OwnTrackingNumber ownTrackingNumber,
        UserId userId,
        CustomerId customerId,
        string consignee,
        string store,
        string? courier,
        string? courierTrackingNumber,
        decimal weight,
        int quantityArticles,
        string description,
        decimal declaredValue,
        decimal taxValue,
        PackageStatus status,
        List<PackageHistory> packageHistories) : base(packageId)
    {
        OwnTrackingNumber = ownTrackingNumber;
        UserId = userId;
        CustomerId = customerId;
        Consignee = consignee;
        Store = store;
        Courier = courier;
        CourierTrackingNumber = courierTrackingNumber;
        Weight = weight;
        QuantityArticles = quantityArticles;
        Description = description;
        DeclaredValue = declaredValue;
        TaxValue = taxValue;
        Status = status;
        _packageHistories = packageHistories;
    }

    public static Package Create(
        PackageId packageId,
        OwnTrackingNumber ownTrackingNumber,
        UserId userId,
        CustomerId customerId,
        string consignee,
        string store,
        string? courier,
        string? courierTrackingNumber,
        decimal weight,
        int quantityArticles,
        string description,
        decimal declaredValue,
        List<PackageHistory>? packageHistories = null)
    {
        
        return new Package(
            packageId,
            ownTrackingNumber,
            userId,
            customerId,
            consignee,
            store,
            courier,
            courierTrackingNumber,
            weight,
            quantityArticles,
            description,
            declaredValue,
            CalcualateTax(declaredValue),
            PackageStatus.PreAlert,
            packageHistories ?? new());
    }

    public void UpdateProperty<T>(Expression<Func<Package, T>> propertyExpression, T newValue)
    {
        var memberExpression = propertyExpression.Body as MemberExpression;
        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo != null && propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(this, newValue);
        }
    }

    public void ChangeCourierInformation(string courier, string trackingNumber)
    {
        Courier = courier;
        CourierTrackingNumber = trackingNumber;
    }

    public void AddCreatedBy(string userName) => CreatedBy = userName;

    public void AddAttachment(string fileName)
    {
        if (!_attachments.Contains(fileName))
        {
            _attachments.Add(fileName);
        }
    }

    public void RemoveAttachment(string fileName)
    {
        if (_attachments.Contains(fileName))
        {
            _attachments.Remove(fileName);
        }
    }

    public void ClearAttachments()
    {
        if (_attachments.Count > 0)
        {
            _attachments.Clear();
        }
    }

    public void AddHistoryToTimeLine(string status)
    {
        _packageHistories.Add(PackageHistory.Create(status));
    }

    public void ChangePackageStatus(PackageStatus newStatus, PackageStatus currentStatus)
    {

        if (newStatus == currentStatus)
        {
            return;
        }

        if (newStatus == PackageStatus.ReceivedInUsa && currentStatus == PackageStatus.PreAlert)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.PreparingForShipment && currentStatus == PackageStatus.ReceivedInUsa)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.InTransitToDestinationCountry && currentStatus == PackageStatus.PreparingForShipment)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.ReceivedAtDestinationCountry && currentStatus == PackageStatus.InTransitToDestinationCountry)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.CustomsClearanceInProcess && currentStatus == PackageStatus.ReceivedAtDestinationCountry)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.ReleasedFromCustoms && currentStatus == PackageStatus.CustomsClearanceInProcess)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.StoredInWarehouse && currentStatus == PackageStatus.ReleasedFromCustoms)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.DeliveredToLocalCourier && currentStatus == PackageStatus.StoredInWarehouse)
        {
            Status = newStatus;
        }

        if (newStatus == PackageStatus.Delivered && currentStatus == PackageStatus.DeliveredToLocalCourier)
        {
            Status = newStatus;
        }

        AddHistoryToTimeLine(newStatus.Name);
    }

    public static decimal CalcualateTax(decimal declaredValue)
    {
        return declaredValue > 150 ? declaredValue * 0.30m : declaredValue * 0.13m;
    }

    public void UdpateTaxValue(decimal declaredValue)
    {
       TaxValue = declaredValue > 150 ? declaredValue * 0.30m : declaredValue * 0.13m;
    }


#pragma warning disable CS8618
    private Package()
    {
    }
    #pragma warning restore CS8618
}

