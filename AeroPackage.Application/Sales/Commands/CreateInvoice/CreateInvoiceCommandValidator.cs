using System;
using FluentValidation;

namespace AeroPackage.Application.Sales.Commands.CreateInvoice;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty();

        RuleFor(r => r.CustomerId)
            .NotEmpty();

        RuleFor(r => r.DateDue)
            .NotEmpty();

        RuleFor(r => r.Reference)
            .MaximumLength(120);

        RuleFor(r => r.Notes)
            .MaximumLength(200);

        RuleFor(r => r.Terms)
            .MaximumLength(200);

        RuleFor(r => r.Subtotal)
            .NotEmpty();

        RuleFor(r => r.Discount)
            .NotEmpty();

        RuleFor(r => r.Tax)
            .NotEmpty();

        RuleFor(r => r.Deposit)
            .NotEmpty();

        RuleFor(r => r.AmountDue)
            .NotEmpty();
    }
}

