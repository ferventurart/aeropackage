using System;
using System.Globalization;
using FluentValidation;

namespace AeroPackage.WebApp.Model.Customer;

public class UpdateCustomerDtoValidator :  AbstractValidator<UpdateCustomerDto>
{
	public UpdateCustomerDtoValidator()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es-MX");

        RuleFor(x => x.Id)
             .NotEmpty();

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

        RuleFor(x => x.Status)
             .InclusiveBetween(0, 1);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateCustomerDto>.CreateWithOptions((UpdateCustomerDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

