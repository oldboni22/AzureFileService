using AzureFileService;
using Microsoft.AspNetCore.Diagnostics;
using OpenTelemetry;

namespace AzureStorageTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddApplicationInsights();
        
        builder.Services.AddAzureFileService(builder.Configuration);
        
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        
        //builder.Services.AddOpenApi();

        var app = builder.Build();
        
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionFeature!.Error;
                
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsJsonAsync(exception.Message);
            });
        });
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.MapOpenApi();
        }
        
        app.UseHttpsRedirection();
        
        app.EnsureContainerExists();

        app.MapControllers();
        app.Run();
    }
}