using System;
using System.Globalization;
using AeroPackage.WebApp.Model.Authentication;
using FluentValidation;

namespace AeroPackage.WebApp.Model.Service;

public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
{
	public CreateServiceDtoValidator()
	{
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es-MX");

        RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(60)
               .WithName("Nombre");

        RuleFor(x => x.Rate)
               .NotEmpty()
               .WithName("Tarifa");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateServiceDto>.CreateWithOptions((CreateServiceDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

