using System;
namespace AeroPackage.Contracts.Sale;

public record CreateInvoiceRequest(Guid UserId,
    Guid CustomerId,
    DateOnly DateDue,
    string? Reference,
    string? Notes,
    string? Terms,
    decimal Subtotal,
    decimal Discount,
    decimal Tax,
    decimal Deposit,
    decimal AmountDue,
    List<InvoiceDetailRequest> InvoiceItems);

public record InvoiceDetailRequest(int? PackageId,
    string Description,
    int Quantity,
    decimal Rate);
