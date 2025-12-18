using System.Reflection.Metadata;
using ApplicationLayer.Contracts;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AzureFileService;

public class AzureFileService(BlobServiceClient blobServiceClient, IOptions<AzureFileServiceOptions> options) : IFileStorageService
{
    private readonly BlobContainerClient _blobContainerClient = blobServiceClient.GetBlobContainerClient(options.Value.ContainerName);

    public void EnsureStorageExists()
    {
        _blobContainerClient.CreateIfNotExists();
    }

    public Task UploadFileAsync(Stream fileStream, string id, string contentType)
    {
        var client = _blobContainerClient.GetBlobClient(id);

        var options = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType,
            }
        };
        
        return client.UploadAsync(fileStream, options);
    }

    public async Task<(Stream, FileMetadata)> GetFileAsync(string id)
    {
        var client = _blobContainerClient.GetBlobClient(id);

        if (!await client.ExistsAsync())
        {
            throw new FileNotFoundException();
        }    
        
        var stream = await client.OpenReadAsync();
        
        var properties = await client.GetPropertiesAsync();
        
        var metadata = new FileMetadata()
        {
            FileName = id,
            ContentType = properties.Value.ContentType,
        };
        
        return (stream, metadata);
    }

    public Task DeleteFileAsync(string id)
    {
        var client = _blobContainerClient.GetBlobClient(id);
        
        return client.DeleteIfExistsAsync();
    }
}
