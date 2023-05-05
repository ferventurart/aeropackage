using System;
namespace AeroPackage.Contracts.Sale;

public record SaleResponse(
    int Id,
    string InvoiceNumber,
    string UserId,
    string CustomerId,
    DateOnly DateIssued,
    DateOnly DateDue,
    string? Reference,
    string? Notes,
    string? Terms,
    decimal Subtotal,
    decimal Discount,
    decimal Tax,
    decimal Deposit,
    decimal AmountDue,
    int Status,
    List<SaleItemResponse> Items);

public record SaleItemResponse(
    string? PackageId,
    string Description,
    int Quantity,
    decimal Rate,
    decimal LineTotal);