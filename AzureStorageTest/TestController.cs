using ApplicationLayer.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageTest;

[ApiController]
[Route("api/test")]
public class TestController(IFileStorageService fileStorageService) : ControllerBase
{
    [HttpPost("upload/{fileName}")]
    public async Task UploadFileAsync([FromForm] IFormFile file, [FromRoute] string fileName)
    {
        await using var stream = file.OpenReadStream();
        await fileStorageService.UploadFileAsync(stream, fileName, file.ContentType);
    }

    [HttpGet("get/{fileName}")]
    public async Task<IActionResult> GetFileAsync([FromRoute] string fileName)
    {
        var output = await fileStorageService.GetFileAsync(fileName);
        
        return new FileStreamResult(output.Content, output.Metadata.ContentType);
    }
}
