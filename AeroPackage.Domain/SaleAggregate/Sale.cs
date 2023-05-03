using System;
using System.Linq.Expressions;
using System.Reflection;
using AeroPackage.Domain.Common.Models;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.Entities;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.SaleAggregate.Entities;
using AeroPackage.Domain.SaleAggregate.Enums;
using AeroPackage.Domain.SaleAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroSale.Domain.SaleAggregate.ValueObjects;

namespace AeroPackage.Domain.SaleAggregate;

public sealed class Sale : AggregateRoot<SaleId, int>
{
    private readonly List<string> _attachments = new();
    private readonly List<SaleItem> _items;

    public UserId UserId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public DateOnly DateIssued { get; private set; }
    public DateOnly DateDue { get; private set; }
    public InvoiceNumber InvoiceNumber { get; private set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public string? Terms { get; set; }
    public decimal Subtotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Deposit { get; private set; }
    public decimal AmountDue { get; private set; }
    public SaleStatus Status { get; private set; }

    public IReadOnlyList<string> Attachments => _attachments.AsReadOnly();
    public IReadOnlyList<SaleItem> Items => _items.AsReadOnly();

    public string CreatedBy { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private Sale(
        SaleId saleId,
        UserId userId,
        CustomerId customerId,
        DateOnly dateIssue,
        DateOnly dateDue,
        InvoiceNumber invoiceNumber,
        string? reference,
        string? notes,
        string? terms,
        decimal subTotal,
        decimal discount,
        decimal tax,
        decimal deposit,
        decimal amountDue,
        List<SaleItem> saleItems) : base(saleId)
    {
        UserId = userId;
        CustomerId = customerId;
        DateIssued = dateIssue;
        DateDue = dateDue;
        InvoiceNumber = invoiceNumber;
        Reference = reference;
        Notes = notes;
        Terms = terms;
        Subtotal = subTotal;
        Discount = discount;
        Tax = tax;
        Deposit = deposit;
        AmountDue = amountDue;
        _items = saleItems;
    }

    public static Sale Create(
        SaleId saleId,
        UserId userId,
        CustomerId customerId,
        DateOnly dateIssue,
        DateOnly dateDue,
        InvoiceNumber invoiceNumber,
        string? reference,
        string? notes,
        string? terms,
        decimal subTotal,
        decimal discount,
        decimal tax,
        decimal deposit,
        decimal amountDue,
        List<SaleItem>? saleItems = null)
    {
        return new Sale(saleId, userId, customerId, dateIssue,
            dateDue, invoiceNumber, reference, notes, terms,
            subTotal, discount, tax, deposit, amountDue, saleItems ?? new());
    }

    public void UpdateProperty<T>(Expression<Func<Sale, T>> propertyExpression, T newValue)
    {
        var memberExpression = propertyExpression.Body as MemberExpression;
        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo != null && propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(this, newValue);
        }
    }

    public void AddSaleItem(PackageId? packageId, string description,
        int quantity, decimal rate)
    {
        _items.Add(SaleItem.Create(packageId, description, quantity, rate));
    }


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
    #pragma warning disable CS8618
    private Sale()
    {
    }
    #pragma warning restore CS8618
}

