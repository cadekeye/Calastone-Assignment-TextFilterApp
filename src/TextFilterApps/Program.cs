using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;
using System.Diagnostics.Metrics;
using TextFilterApps.Application.Contracts;
using TextFilterApps.Application.DependencyInjection;
using TextFilterApps.Application.Middleware;
using TextFilterApps.Infrastructure.Services;
using TextFilterApps.Presentation.FilterApp;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

Meter TextFilterAppMeter = new("Calastone.Challenge.TextFilterApp", "1.0");
Counter<long> TextFilterAppCounter = TextFilterAppMeter.CreateCounter<long>("TextFilterAppCounter");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter();
            })
            .WithMetrics(metricsProviderBuilder =>
            {
                metricsProviderBuilder
                .AddAspNetCoreInstrumentation()
                .AddMeter("Calastone.Challenge.TextFilterApp")
                .AddConsoleExporter();
            });
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddSingleton(Log.Logger);
        services.AddScoped<IFileReaderService, FileReaderService>();
        services.AddScoped<IFilterApp, FilterApp>();
        services.AddApplicationService();
    })
    .UseSerilog()
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    Log.Logger.Information("Application starting ...");

    await services.GetRequiredService<IFilterApp>().Handle();

    Log.Logger.Information("Application stopped running ...");
    Console.ReadKey();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}