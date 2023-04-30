using System;
using AeroPackage.WebApp.Model.Authentication;
using FluentValidation;

namespace AeroPackage.WebApp.Model.Customer;

public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
	public CreateCustomerDtoValidator()
	{
        RuleFor(x => x.FirstName)
               .NotEmpty()
               .MaximumLength(100)
               .WithName("Nombre");

        RuleFor(x => x.LastName)
               .NotEmpty()
               .MaximumLength(100)
               .WithName("Apellido");

        RuleFor(x => x.Email)
               .NotEmpty()
               .MaximumLength(250)
               .EmailAddress();

        RuleFor(x => x.CellPhoneNumber)
               .NotEmpty()
               .Matches(@"^(?:-*\d-*){8}$")
               .WithName("Celular");

        RuleFor(x => x.Gender)
              .InclusiveBetween(1, 2)
              .WithMessage("Por favor seleccione un género válido.")
              .WithName("Género");

        RuleFor(x => x.Identification)
              .NotEmpty()
              .Matches(@"^\d{8}-\d{1}$")
              .WithName("Dui");

        RuleFor(x => x.Address)
              .NotEmpty()
              .MaximumLength(200)
              .WithName("Direccion");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateCustomerDto>.CreateWithOptions((CreateCustomerDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

