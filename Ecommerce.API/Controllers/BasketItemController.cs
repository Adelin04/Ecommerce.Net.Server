using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

// [Authorize(Roles = "ADMIN")]
[Route("api/[Controller]/v1/")]
[ApiController]
public class BasketItemController : ControllerBase
{

    private readonly BasketItemsService _basketItemsService;
    private readonly BasketServices _basketServices;
    private readonly ILogger<BasketItemController> _logger;

    public BasketItemController(BasketItemsService basketItemsService, BasketServices basketServices, ILogger<BasketItemController> logger)
    {
        this._basketItemsService = basketItemsService;
        this._basketServices = basketServices;
        this._logger = logger;
    }

    [HttpPost("add/newBasketItem")]
    public async Task<ActionResult> AddNewBasketItem(RequestRegisterBasketItem requestRegisterBasketItem)
    {
        /*  try
         {
             var newBasketItemCreated = await this._basketItemsService.AddNewBasketItem_ServiceAsync(new BasketItems() { Quantity = requestRegisterBasketItem.Quantity, ProductId = requestRegisterBasketItem.ProductId });
             var newBasketCrated = await this._basketServices.AddNewBasket_ServiceAsync(new Basket() { BuyerId = requestRegisterBasketItem.BuyerId });

             if (newBasketItemCreated is not null || newBasketCrated is not null)
             {
                 this._logger.LogInformation($"New items successfully added");
                 return Ok(new { Success = true, NewBasketItemCreated = newBasketItemCreated });
             }
         }
         catch (Exception exception)
         {
             Console.WriteLine("Error -> " + exception.Message);
             this._logger.LogInformation("Error -> " + exception.Message);
             return BadRequest(new { Error = exception.Message });
         } */

        return BadRequest(new { Success = false, Message = "Something went wrong!" });
    }

    [HttpGet("get/allBasketItems")]
    public async Task<ActionResult> GetAllBasketItems()
    {
        try
        {
            var listOfBasketItems = await this._basketItemsService.GetAllBasketItems_ServiceAsync();

            if (listOfBasketItems is not null)
            {
                this._logger.LogInformation($"Returned basket items list");
                return Ok(new { Success = true, Baskets = listOfBasketItems, Count = listOfBasketItems.Count });
            }

            if (listOfBasketItems.Count < 1)
            {
                this._logger.LogInformation($"Returned baskets items list");
                return Ok(new { Message = $"Baskets items list is empty -> {listOfBasketItems.Count}" });
            }
        }
        catch (Exception exception)
        {
            this._logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "Baskets items list could not be returned!" });
    }

    [HttpDelete("delete/basketItemById/{id}")]
    public async Task<ActionResult> DeleteBasketItemById([FromRoute] long id)
    {
        try
        {
            var basketItemDeleted = await this._basketItemsService.DeleteBasketItemByProductId_ServiceAsync(id);

            if (basketItemDeleted is not null)
            {
                this._logger.LogInformation($"Basket with {id} was successfully deleted");
                return Ok(new { Success = true, BasketItemDeleted = basketItemDeleted });
            }
        }
        catch (System.Exception exception)
        {
            this._logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }
        return BadRequest(new { Success = false, Message = "Baskets item could not be deleted!" });
    }

    [HttpPost("decrement/quntity/basketItemById/{id}/{size}")]
    public async Task<ActionResult> DecrementItemQuantity([FromRoute] long id, [FromRoute] string size)
    {

        try
        {
            var itemDecremented = await this._basketItemsService.DecrementBasketItemQuantityById(id, size);

            if (itemDecremented is not null)
            {
                this._logger.LogInformation($"Basket with {id} was successfully deleted");
                return Ok(new { Success = true, ItemDecremented = itemDecremented });
            }
        }
        catch (System.Exception exception)
        {
            this._logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }
        return BadRequest(new { Success = false, Message = "Baskets item could not be deleted!" });

    }

}

