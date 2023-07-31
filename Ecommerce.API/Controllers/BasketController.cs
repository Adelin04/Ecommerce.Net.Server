using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Ecommerce.API.Controllers;

[Route("api/[Controller]/v1/")]
[ApiController]
public class BasketController : ControllerBase
{

    private readonly BasketServices _basketServices;
    private readonly ILogger<BasketController> _logger;

    public BasketController(BasketServices basketServices, ILogger<BasketController> logger)
    {
        this._basketServices = basketServices;
        this._logger = logger;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("get/allBaskets")]
    public async Task<ActionResult> GetAllBaskets()
    {
        try
        {
            var listOfBaskets = await this._basketServices.GetAllBaskets_ServiceAsync();

            if (listOfBaskets is not null)
            {
                this._logger.LogInformation($"Returned baskets list");
                return Ok(new { Success = true, Baskets = listOfBaskets, Count = listOfBaskets.Count });
            }

            if (listOfBaskets.Count < 1)
            {
                this._logger.LogInformation($"Returned baskets list");
                return Ok(new { Message = $"Baskets list is empty -> {listOfBaskets.Count}" });
            }
        }
        catch (Exception exception)
        {
            this._logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "Baskets list could not be returned!" });
    }

    [HttpPost("add/newBasket")]
    public async Task<ActionResult> AddNewBasket([FromBody] RequestRegisterBasket requestRegisterBasket)
    {
        try
        {
            var newBasketCrated = await this._basketServices.AddNewBasket_ServiceAsync(requestRegisterBasket);

            if (newBasketCrated is not null)
            {
                this._logger.LogInformation($"New basket successfully created");
                return Ok(new { Success = true, NewBasketCreated = newBasketCrated });
            }

        }
        catch (System.Exception exception)
        {
            this._logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }
        return BadRequest(new { Success = false, Message = "Basket could not be created!" });
    }

    [HttpPost("get/basketByUserEmail/{userEmail}")]
    public async Task<ActionResult> GetBasketByUserEmail([FromRoute] string userEmail)
    {

        try
        {
            var basketByUserEmail = await this._basketServices.GetBasketByUserEmail_ServiceAsync(userEmail);

            if (basketByUserEmail is not null)
            {
                this._logger.LogInformation("The basket given by the user id");
                return Ok(new { Success = true, BasketByUserEmail = basketByUserEmail });
            }

        }
        catch (System.Exception exception)
        {
            this._logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });

        }
        return BadRequest(new { Success = false, Message = "The Basket could not be found!" });
    }

    [HttpDelete("delete/basketById/{id}")]

    public async Task<ActionResult> DeleteBasketById(long id)
    {
        try
        {
            var basketDeleted = await this._basketServices.DeleteBasketById(id);
            if (basketDeleted is not null)
            {
                this._logger.LogInformation($"Basket with id {id} was successfully deleted");
                return Ok(new { Success = true, BasketDeleted = basketDeleted });
            }
        }
        catch (System.Exception exception)
        {
            this._logger.LogInformation($"{exception.Message}");
            return BadRequest(new { Success = false, Message = $"{exception.Message}" });
        }

        this._logger.LogInformation($"The basket with id {id} could not be found");
        return NotFound(new { Success = false, Message = $"The basket with id {id} could not be found" });
    }

    [HttpDelete("delete/basketByUserEmail/{email}")]

    public async Task<ActionResult> DeleteBasketByUserEmail(string email)
    {
        try
        {
            var basketDeleted = await this._basketServices.DeleteBasketByUserEmail(email);
            if (basketDeleted is not null)
            {
                this._logger.LogInformation($"Basket with id {basketDeleted.Id} was successfully deleted");
                return Ok(new { Success = true, BasketDeleted = basketDeleted });
            }
        }
        catch (System.Exception exception)
        {
            this._logger.LogInformation($"{exception.Message}");
            return BadRequest(new { Success = false, Message = $"{exception.Message}" });
        }

        this._logger.LogInformation($"The basket could not be found");
        return NotFound(new { Success = false, Message = $"The basket could not be found" });
    }
}