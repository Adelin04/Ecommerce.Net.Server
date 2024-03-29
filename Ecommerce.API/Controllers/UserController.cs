﻿using Ecommerce.API.Contracts;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
// [Authorize(Roles = "ADMIN")]
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

        return NotFound(new { Success = false, Message = "The users list not found!" });
    }

    [HttpGet("get/userById/{id}")]
    public async Task<ActionResult> GetUserById([FromRoute] long id)
    {
        ModelState.Remove("password");
        try
        {
            var userById = await this._userService.GetUserById(id);

            if (userById is not null)
            {
                this.Logger.LogInformation($"The user was successfully returned " + userById);
                return Ok(new { Success = true, UserById = userById });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
        }

        return NotFound(new { Success = false, Message = "The user not found!" });
    }

    [HttpGet("get/userByEmail/{email}")]
    public async Task<ActionResult> GetUserByemail(string email)
    {
        ModelState.Remove("password");
        try
        {
            var userByEmail = await this._userService.GetUserByEmail(email);

            if (userByEmail is not null)
            {
                this.Logger.LogInformation($"The user was successfully returned " + userByEmail);
                return Ok(new { Success = true, UserById = userByEmail });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
        }

        return NotFound(new { Success = false, Message = "The user not found!" });
    }

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

        return NotFound(new { Success = false, Message = "The user not found!" });
    }

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