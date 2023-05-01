using System;
using FluentValidation;
using System.Globalization;

namespace AeroPackage.WebApp.Model.User;

	public class UpdateUserProfileDtoValidator : AbstractValidator<UpdateUserProfileDto>
{
    public UpdateUserProfileDtoValidator()
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

        RuleFor(x => x.Password)
               .MaximumLength(40)
               .WithName("Contrasena");

        RuleFor(x => x.ConfirmPassword)
               .MaximumLength(40)
               .Equal(x => x.Password)
               .WithMessage("Las contrasenas no coinciden")
               .WithName("Confirmar Contrasena");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateUserProfileDto>.CreateWithOptions((UpdateUserProfileDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

