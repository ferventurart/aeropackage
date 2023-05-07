using System;
namespace AeroPackage.WebApp.Model.Sale;

public class InvoiceDto
{
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public DateOnly DateDue { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public string Terms { get; set; }
    public decimal Subtotal => Items is not null && Items.Count > 0 ? Items.Sum(s => s.LineTotal) : 0;
    public decimal Discount => Items is not null && Items.Count > 0 ? Items.Sum(s => s.Discount) : 0;
    public decimal Tax { get; set; }
    public decimal Deposit { get; set; }
    public decimal AmountDue => Subtotal - Discount - Deposit;
    public List<InvoiceDetailDto> Items = new();

}

public class InvoiceDetailDto
{
    public Guid Id { get; set; }
    public int? PackageId { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Discount { get; set; }
    public decimal LineTotal => (Quantity * Rate) - Discount;
}

