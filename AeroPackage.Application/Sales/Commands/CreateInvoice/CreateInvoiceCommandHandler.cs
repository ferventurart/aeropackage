using AeroPackage.Domain.Common.DomainErrors;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Domain.CustomerAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.SaleAggregate;
using AeroPackage.Domain.SaleAggregate.Entities;
using AeroPackage.Domain.SaleAggregate.ValueObjects;
using AeroPackage.Domain.UserAggregate.ValueObjects;
using AeroSale.Domain.SaleAggregate.ValueObjects;
using System;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Sales.Commands.CreateInvoice;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, ErrorOr<Sale>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPackageRepository _packageRepository;

    public CreateInvoiceCommandHandler(ISaleRepository saleRepository, IPackageRepository packageRepository)
    {
        _saleRepository = saleRepository;
        _packageRepository = packageRepository;
    }

    public async Task<ErrorOr<Sale>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = CustomerId.Create(command.CustomerId);
        var userId = UserId.Create(command.UserId);
        var lastInsertedId = await _saleRepository.GetLastInsertedId();

        var invoiceNumber = InvoiceNumber.Create(lastInsertedId);

        if (command.InvoiceItems.Count == 0)
        {
            return Errors.Sale.NoSaleItems;
        }

        List<SaleItem> items = new();

        foreach (var item in command.InvoiceItems)
        {
            items.Add(SaleItem.Create(item.PackageId
            is not null ? PackageId.Create(item.PackageId.Value) : null,
            item.Description, item.Quantity, item.Rate));
        }

        var sale = Sale.Create(SaleId.Create(0), userId, customerId,
            new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
            command.DateDue, invoiceNumber, command.Reference,
            command.Notes, command.Terms, command.Subtotal,
            command.Discount, command.Tax, command.Deposit,
            command.AmountDue, items);

        _saleRepository.Add(sale);

        return sale;
    }
}

