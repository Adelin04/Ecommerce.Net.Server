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
                return Ok(new { Success = true, UserCreated = userCreated });
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "Users already exist!" });
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserDataLogin userDataLogin)
    {

        try
        {
            var token = await this._authService.Login(userDataLogin);

            if (token is not null)
                return Ok(new { Success = true, Token = token });
            return BadRequest(new { Success = false, Message = "There is no account with this email address yet !" });
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }
    }
}