using System;
using System.Globalization;
using FluentValidation;

namespace AeroPackage.WebApp.Model.Authentication;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es-MX");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Debe ingresar un email valido.")
            .NotEmpty().WithMessage("El email es un campo requerido.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contrasena es un campo requerido.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<LoginDto>.CreateWithOptions((LoginDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

