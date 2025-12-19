using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AzureStorageTest;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationInsights(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry().UseAzureMonitor();
    }
}
