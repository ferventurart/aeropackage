using System;
using System.Globalization;
using FluentValidation;

namespace AeroPackage.WebApp.Model.Service;

public class UpdateServiceDtoValidator :  AbstractValidator<UpdateServiceDto>
{
	public UpdateServiceDtoValidator()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es-MX");

        RuleFor(x => x.Id)
             .NotEmpty();

        RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(60)
               .WithName("Nombre");

        RuleFor(x => x.Rate)
               .NotEmpty()
               .WithName("Tarifa");

        RuleFor(x => x.Status)
             .InclusiveBetween(0, 1);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateServiceDto>.CreateWithOptions((UpdateServiceDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

