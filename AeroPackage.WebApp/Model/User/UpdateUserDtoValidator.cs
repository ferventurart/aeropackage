using System;
using FluentValidation;

namespace AeroPackage.WebApp.Model.User;

public class UpdateUserDtoValidator :  AbstractValidator<UpdateUserDto>
{
	public UpdateUserDtoValidator()
    {
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

        RuleFor(x => x.Password)
               .NotEmpty()
               .MaximumLength(40)
               .WithName("Contrasena");

        RuleFor(x => x.Role)
              .InclusiveBetween(1, 3)
              .WithMessage("Por favor seleccione un rol válido.")
              .WithName("Rol");

        RuleFor(x => x.Status)
             .InclusiveBetween(0, 1);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateUserDto>.CreateWithOptions((UpdateUserDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

