using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Authorize(Roles = "ADMIN")]
[ApiController]
[Route("api/[controller]/v1")]
public class SizeStockController : ControllerBase
{
    private readonly SizeStockService _sizeStockService;
    private readonly Logger<SizeStockController> Logger;
    public SizeStockController(SizeStockService sizeStockService, Logger<SizeStockController> logger)
    {
        this._sizeStockService = sizeStockService;
        this.Logger = logger;
    }

    [HttpPost("add/new/size/existProduct/{id}/{stock}/{size}")]
    public async Task<ActionResult> AddNewSizeAndStockExistProduct([FromRoute] long id, long stock, string size)
    {
        var newSizeStockAdded = await this._sizeStockService.AddNewSizeAndStockExistProduct_ServiceAsync(id, stock, size);
        try
        {
            if (newSizeStockAdded is not null)
            {
                this.Logger.LogInformation($"The size {size} has been successfully added to product id {id}");
                return Ok(new { Success = true, NewSizeStockAdded = newSizeStockAdded });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }


        this.Logger.LogInformation($"The new size and stock could not be added to product with id {id}!");
        return BadRequest(new { Success = false, Message = $"The new size and stock could not be added to product with id {id}!" });
    }
}