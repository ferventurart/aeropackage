using System;
using AeroPackage.Domain.SaleAggregate;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Sales.Commands.CreateInvoice;

public record CreateInvoiceCommand(
    Guid UserId,
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
    List<InvoiceItemCommand> InvoiceItems) : IRequest<ErrorOr<Sale>>;


public record InvoiceItemCommand(
    int? PackageId,
    string Description,
    int Quantity,
    decimal Rate);