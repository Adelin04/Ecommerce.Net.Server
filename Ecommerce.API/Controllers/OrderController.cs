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
        await this._orderService.CreateNewOrder(requestRegisterOrder);
        return Ok(new { Success = true });
    }
}