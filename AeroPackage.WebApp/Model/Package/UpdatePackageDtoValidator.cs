using System;
using System.Globalization;
using FluentValidation;

namespace AeroPackage.WebApp.Model.Package;

public class UpdatePackageDtoValidator : AbstractValidator<UpdatePackageDto>
{
    public UpdatePackageDtoValidator()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es-MX");

        RuleFor(x => x.Store)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(x => x.Courier)
           .MaximumLength(100);

        RuleFor(x => x.CourierTrackingNumber)
           .MaximumLength(100)
           .WithName("Numero de Tracking");

        RuleFor(x => x.Weight)
            .NotEmpty()
            .GreaterThan(0.00M)
            .WithName("Peso");

        RuleFor(x => x.QuantityArticles)
            .NotEmpty()
            .GreaterThan(0)
            .WithName("\u2116 de Articulos");

        RuleFor(x => x.Description)
           .MaximumLength(100)
           .NotEmpty()
           .WithName("Descripcion");

        RuleFor(x => x.DeclaredValue)
            .NotEmpty()
            .GreaterThan(0.00M)
            .WithName("Valor del Paquete");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdatePackageDto>.CreateWithOptions((UpdatePackageDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

