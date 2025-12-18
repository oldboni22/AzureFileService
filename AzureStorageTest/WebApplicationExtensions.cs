using ApplicationLayer.Contracts;

namespace AzureStorageTest;

public static class WebApplicationExtensions
{
    public static void EnsureContainerExists(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var fileService = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
        fileService.EnsureStorageExists();
    }
}