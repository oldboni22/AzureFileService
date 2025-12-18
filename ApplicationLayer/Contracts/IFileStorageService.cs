using Microsoft.AspNetCore.Http;

namespace ApplicationLayer.Contracts;

public interface IFileStorageService
{
    void EnsureStorageExists();
    
    Task UploadFileAsync(Stream fileStream, string fileName, string contentType);
    
    Task<(Stream, FileMetadata)> GetFileAsync(string fileName);
    
    Task DeleteFileAsync(string fileName);    
}
