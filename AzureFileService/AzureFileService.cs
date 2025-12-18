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

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType,
            }
        };
        
        return client.UploadAsync(fileStream, uploadOptions);
    }

    public async Task<FileOutput> GetFileAsync(string id)
    {
        var client = _blobContainerClient.GetBlobClient(id);

        if (!await client.ExistsAsync())
        {
            throw new FileNotFoundException();
        }    
        
        var downloadResponse = await client.DownloadStreamingAsync();
        
        var contentType = downloadResponse.Value.Details.ContentType;
        
        var metadata = new FileMetadata()
        {
            FileName = id,
            ContentType = contentType
        };
        
        return new FileOutput
        {
            Content = downloadResponse.Value.Content,
            Metadata = metadata
            
        };
    }

    public Task DeleteFileAsync(string id)
    {
        var client = _blobContainerClient.GetBlobClient(id);
        
        return client.DeleteIfExistsAsync();
    }
}
