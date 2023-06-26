using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;

namespace Ecommerce.API.Controllers;

[Authorize(Roles = "ADMIN")]
[ApiController]
[Route("api/[controller]/v1")]
public class SizeStockController : ControllerBase
{
    private readonly SizeStockService _sizeStockService;
    private readonly ILogger<SizeStockController> Logger;
    public SizeStockController(SizeStockService sizeStockService, ILogger<SizeStockController> logger)
    {
        this._sizeStockService = sizeStockService;
        this.Logger = logger;
    }

    [HttpPost("add/newSize/existProduct")]
    public async Task<ActionResult> AddNewSizeAndStockExistProduct([FromBody] RequestRegisterSizeStock requestRegisterSizeStock)
    {
        try
        {
            foreach (var item in requestRegisterSizeStock.listOfNewSizeStock)
            {

                var newSizeStockAdded = await this._sizeStockService.AddNewSizeAndStockExistProduct_ServiceAsync(requestRegisterSizeStock.idProduct, (long)item["stock"], (string)item["size"]);

                System.Console.WriteLine("-------------------------------------------------------------------newSizeStockAdded " + newSizeStockAdded);

                // if (newSizeStockAdded is not null)
                // {
                // }
                this.Logger.LogInformation($"The size {item["size"]} has been successfully added to product id {requestRegisterSizeStock.idProduct}");
            }
            this.Logger.LogInformation($"The size(s) has been successfully added to product id {requestRegisterSizeStock.idProduct}");
            return Ok(new { Success = true, NewSizeStockAdded = requestRegisterSizeStock.listOfNewSizeStock });
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message);
        }

        this.Logger.LogInformation($"The new size and stock could not be added to product with id {requestRegisterSizeStock.listOfNewSizeStock}!");
        return BadRequest(new { Success = false, Message = $"The new size and stock ${requestRegisterSizeStock.listOfNewSizeStock} could not be added to product with id ${requestRegisterSizeStock}!" });
    }
}