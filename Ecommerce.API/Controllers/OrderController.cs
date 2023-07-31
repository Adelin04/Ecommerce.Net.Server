using Ecommerce.API.Services;
using Ecommerce.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]

public class OrderController : ControllerBase
{

    private OrderService _orderService;
    private ILogger<OrderController> Logger;

    public OrderController(OrderService orderService, ILogger<OrderController> logger)
    {
        this._orderService = orderService;
        this.Logger = logger;
    }

    [HttpPost("dataOrder")]
    public async Task<ActionResult> CreateNewOrderService_Async(RequestRegisterOrder requestRegisterOrder)
    {
        try
        {
            var orderCreated = await this._orderService.CreateNewOrder(requestRegisterOrder);

            if (orderCreated is not null)
            {
                this.Logger.LogInformation("A new product has been successfully created -> " + orderCreated);
                return Ok(new { Success = true, NewOrder = orderCreated });
            }
        }
        catch (System.Exception exception)
        {
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Success = false, Error = exception.Message });
        }
        return BadRequest(new
        { Success = false, Message = $"The order could not be created!" });
    }
}