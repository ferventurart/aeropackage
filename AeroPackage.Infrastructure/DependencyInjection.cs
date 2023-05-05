using System.Text;

using AeroPackage.Application.Common.Interfaces.Authentication;
using AeroPackage.Application.Common.Interfaces.Persistence;
using AeroPackage.Application.Common.Interfaces.Services;
using AeroPackage.Infrastructure.Authentication;
using AeroPackage.Infrastructure.Persistence;
using AeroPackage.Infrastructure.Persistence.Repositories;
using AeroPackage.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;

namespace AeroPackage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistance(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<ICourierProvider, CourierProvider>();
        services.AddSingleton<IExcelService, ExcelService>();

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        return services;
    }

    public static IServiceCollection AddPersistance(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AeroPackageDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("Database")));

        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IPackageRepository, PackageRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });

        return services;
    }
}