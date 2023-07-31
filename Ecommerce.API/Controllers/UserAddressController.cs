
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class UserAddressController : ControllerBase
{
    private readonly UserAddressesService _userAddressesService;
    private readonly ILogger<UserAddressController> Logger;

    public UserAddressController(UserAddressesService userAddressesService, ILogger<UserAddressController> logger)
    {
        this._userAddressesService = userAddressesService;
        this.Logger = logger;
    }

    [HttpGet("get/UserAddressByUserId")]
    public async Task<ActionResult> GetUserAddressByUserId(long id)
    {
        try
        {
            var existUserAddress = await this._userAddressesService.GetUserAddressesByUserId_ServiceAsync(id);

            if (existUserAddress is not null)
            {
                this.Logger.LogInformation("Returned user address by user id" + existUserAddress);
                return Ok(new { Success = true, UserAddress = existUserAddress });
            }

        }
        catch (System.Exception exception)
        {
            this.Logger.LogInformation("Error" + exception.Message);
            return BadRequest(new { Success = false, ErrorMsg = exception.Message });
        }

        this.Logger.LogInformation("Not found any address for this id");
        return NotFound(new { Success = false, Message = "Not found any address for this id" });
    }
}