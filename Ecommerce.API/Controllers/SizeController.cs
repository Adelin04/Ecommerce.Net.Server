using Ecommerce.API.Services;
using Ecommerce.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1/")]
public class SizeController : ControllerBase
{
    private readonly SizeService _sizeService;
    private readonly ILogger<ControllerBase> Logger;

    public SizeController(SizeService sizeService, ILogger<ControllerBase> logger)
    {
        this._sizeService = sizeService;
        this.Logger = logger;
    }

    [HttpGet("get/allSizes")]
    public async Task<ActionResult> GetAllSizes()
    {
        try
        {
            var allSizes = await this._sizeService.GetAllSize_ServiceAsync();

            if (allSizes is not null)
            {
                Logger.LogInformation($"Returned all sizes {allSizes}");
                return Ok(new { Success = true, Sizes = allSizes, NrSizes = allSizes.Count });
            }
        }
        catch (Exception exception)
        {
            Logger.LogInformation($"Error -> {exception.Message}");
        }
        Logger.LogInformation($"The sizes entity could not be found!");
        return NotFound(new { Success = false });
    }

    [HttpPost("create/newSize")]
    public async Task<ActionResult> CreateNewSize(SizeDataRegister sizeDataRegister)
    {
        try
        {
            var newSizeCreated = await this._sizeService.CreateNewSize(sizeDataRegister);

            if (newSizeCreated is not null)
            {
                this.Logger.LogInformation($"New size was created -> {newSizeCreated}");
                return Ok(new { Success = true, NewSizeCreated = newSizeCreated });
            }

        }
        catch (System.Exception exception)
        {
            this.Logger.LogInformation($"Error -> {exception.Message}");
        }

        this.Logger.LogInformation($"The size '{sizeDataRegister.Name}' was not could be created.");
        return BadRequest(new { Success = false, Message = $"The size '{sizeDataRegister.Name}' was not could be created." });
    }
}