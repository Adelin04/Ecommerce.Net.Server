using Ecommerce.API.Services;
using Ecommerce.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> Logger;

    public AuthController(AuthService authService, ILogger<AuthController> logger)
    {
        this._authService = authService;
        this.Logger = logger;
    }
    
    [HttpPost("register/newUser")]
    public async Task<ActionResult> CreateNewUser([FromBody] UserDataRegister userDataRegister)
    {
        try
        {
            var userCreated = await this._authService.Register(userDataRegister);

            if (userCreated is not null)
                return Ok(new {Success = true, UserCreated = userCreated});
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new {Error = exception.Message});
        }

        return BadRequest(new {Success = false, Message = "User already exist!"});
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserDataLogin userDataLogin)
    {
        try
        {
            var response = await this._authService.Login(userDataLogin);

            if (response[0] is not null)
                return Ok(new
                    {Success = true, Token = response[0], BasketByUser = response[1], AddressUser = response[2]});
            return BadRequest(new {Success = false, Message = "The email or password is wrong!"});
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new {Error = exception.Message});
        }
    }
}