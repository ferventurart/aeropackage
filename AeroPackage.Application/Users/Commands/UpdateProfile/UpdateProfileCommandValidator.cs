using System;
using FluentValidation;

namespace AeroPackage.Application.Users.Commands.UpdateProfile;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
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

        RuleFor(x => x.Password)
               .MaximumLength(40)
               .WithName("Contrasena");

        RuleFor(x => x.ConfirmPassword)
               .MaximumLength(40)
               .Equal(x => x.Password)
               .WithName("Confirmar Contrasena");
    }
}

