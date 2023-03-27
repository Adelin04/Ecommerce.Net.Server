using Ecommerce.API.Contracts;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> Logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        this._userService = userService;
        this.Logger = logger;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("getAllUsers")]
    public async Task<ActionResult> GetAllUsers()
    {
        try
        {
            var listAllUsers = await this._userService.GetAllUsers();

            if (listAllUsers is not null)
            {
                this.Logger.LogInformation($"Returned users list");
                return Ok(new { Success = true, Users = listAllUsers, CounterUser = listAllUsers.Count });
            }

            if (listAllUsers.Count < 1)
            {
                this.Logger.LogInformation($"Returned users list");
                return Ok(new { Message = $"Users list is empty -> {listAllUsers.Count}" });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "No users found!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("get/userById/{id}")]
    public async Task<ActionResult> GetUserById([FromRoute] long id)
    {
        try
        {
            var userById = await this._userService.GetUserById(id);

            if (userById is not null)
                return Ok(new { Success = true, userById = userById });
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
        }

        return BadRequest(new { Success = false, Message = "No user found!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("update/userById/{id}")]
    public async Task<ActionResult> UpdateUserById([FromRoute] long id, [FromBody] UserDataUpdate userDataUpdate)
    {
        try
        {
            var userById = await this._userService.GetUserById(id);

            if (userById is not null)
            {
                var userUppdated = await this._userService.UpdateUserById(id, userDataUpdate);
                return Ok(new { Success = true, UserUpdated = userUppdated });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "No user found!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("delete/userById/{id}")]
    public async Task<ActionResult> DeleteUserById([FromRoute] long id)
    {
        try
        {
            var userById = await this._userService.GetUserById(id);

            if (userById is not null)
            {
                var userRemoved = await this._userService.DeleteUserById(id);
                return Ok(new { Success = true, UserRemoved = userRemoved });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "No user found!" });
    }
}