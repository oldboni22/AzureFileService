using ApplicationLayer.Contracts;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFileService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureFileService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration[$"{AzureFileServiceOptions.SectionName}:{AzureFileServiceOptions.ConnectionStringName}"];
        
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(connectionString);
        });
        
        return 
            services
                .Configure<AzureFileServiceOptions>(configuration.GetSection(AzureFileServiceOptions.SectionName))
                .AddScoped<IFileStorageService, AzureFileService>();
    }
}