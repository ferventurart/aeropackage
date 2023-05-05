using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using AeroPackage.WebApp;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AeroPackage.WebApp.Providers;
using AeroPackage.WebApp.Services.Interfaces;
using AeroPackage.WebApp.Services;
using FluentValidation;
using Mapster;
using MapsterMapper;
using AeroPackage.WebApp.Providers.Interfaces;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiUrl"])
});

builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<IBrowserFileProvider, BrowserFileProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<ICompanyServicesService, CompanyServicesService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddValidatorsFromAssembly(typeof(App).Assembly);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

await builder.Build().RunAsync();