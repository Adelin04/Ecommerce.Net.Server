using Ecommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[Controller]/v1")]
public class AWSController : ControllerBase
{
    private readonly AwsS3StorageImagesService _awsS3StorageImagesService;

    public AWSController(AwsS3StorageImagesService awsS3StorageImagesService)
    {
        this._awsS3StorageImagesService = awsS3StorageImagesService;
    }


    [HttpPost]
    public async Task<ActionResult> UploadFile(IFormFile file)
    {
        return null;
    }
}