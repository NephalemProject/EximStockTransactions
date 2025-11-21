using System;
using System.Globalization;
using System.Net.Http;
using EximStockTransactions.Web.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EximStockTransactions.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register Web services
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<NotificationService>();

// Configure HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient
{
  // WARNING: Replace with your actual deployment URL later
  BaseAddress = new Uri("https://localhost:7000")
});

// Set culture to ZA globally
var culture = new CultureInfo("en-ZA");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();