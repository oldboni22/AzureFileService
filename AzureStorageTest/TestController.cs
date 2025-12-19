using ApplicationLayer.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageTest;

[ApiController]
[Route("api/test")]
public class TestController(IFileStorageService fileStorageService) : ControllerBase
{
    [HttpPost("{fileName}")]
    public async Task UploadFileAsync(IFormFile file, [FromRoute] string fileName)
    {
        await using var stream = file.OpenReadStream();
        await fileStorageService.UploadFileAsync(stream, fileName, file.ContentType);
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetFileAsync([FromRoute] string fileName)
    {
        var output = await fileStorageService.GetFileAsync(fileName);
        
        return new FileStreamResult(output.Content, output.ContentType);
    }
    
    [HttpGet("link/{fileName}")]
    public async Task<string> GetFileLinkAsync([FromRoute] string fileName)
    {
        return await fileStorageService.GetFileLinkAsync(fileName);
    }
    
    [HttpDelete("{fileName}")]
    public async Task DeleteFileAsync([FromRoute] string fileName)
    {
        await fileStorageService.DeleteFileAsync(fileName);
    }
}
