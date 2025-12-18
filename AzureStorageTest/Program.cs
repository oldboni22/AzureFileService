using AzureFileService;

namespace AzureStorageTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAzureFileService(builder.Configuration);
        
        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.UseHttpsRedirection();
        
        app.EnsureContainerExists();

        app.MapControllers();
        app.Run();
    }
}