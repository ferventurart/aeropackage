using AeroPackage.Api;
using AeroPackage.Api.Handlers;
using AeroPackage.Api.Handlers.Interfaces;
using AeroPackage.Application;
using AeroPackage.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<IFileHandler, FileHandler>();

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseResponseCompression();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseRouting();

    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
        RequestPath = new PathString("/uploads")
    });

    app.UseCors();

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}