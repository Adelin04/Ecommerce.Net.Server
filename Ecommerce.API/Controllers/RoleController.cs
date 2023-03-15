using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;
    private readonly ILogger<RoleController> Logger;

    public RoleController(RoleService roleService, ILogger<RoleController> logger)
    {
        this.Logger = logger;
        this._roleService = roleService;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("create/newRole/")]
    public async Task<ActionResult> CreateNewRole([FromBody] RoleDataCreateNewRole dataCreateNewRole)
    {
        try
        {
            var newRoleCreated = await this._roleService.CreateNewRole_ServiceAsync(dataCreateNewRole);

            if (newRoleCreated is not null)
            {
                this.Logger.LogInformation($"New role was created -> {dataCreateNewRole.nameRole}");
                return Ok(new { Success = true, RoleCreated = dataCreateNewRole.nameRole });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }


        this.Logger.LogInformation($"The role {dataCreateNewRole.nameRole} could not be created!");
        return BadRequest(new
            { Success = false, Message = $"The role {dataCreateNewRole.nameRole} could not be created!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("get/roleById/{id}")]
    public async Task<ActionResult> GetRoleById(long id)
    {
        try
        {
            var roleById = await this._roleService.GetRoleById_ServiceAsync(id);
            
            if (roleById is not null)
            {
                this.Logger.LogInformation($"Role by id -> {roleById.Id}");
                return Ok(new { Success = true, RoleById = roleById });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation($"Error -> {exception.Message.ToString()} ");
        }

        return BadRequest(new { Success = false, Message = $"Role by id not found!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("get/allRoles")]
    public async Task<ActionResult> GetAllRoles()
    {
        try
        {
            var allRoles = await this._roleService.GetAllRoles_ServiceAsync();

            if (allRoles is not null)
            {
                this.Logger.LogInformation($"Roles list");
                return Ok(new { Success = true, AllRoles = allRoles, Count = allRoles.Count });
            }

            if (allRoles.Count < 1)
            {
                this.Logger.LogInformation($"Returned roles list");
                return Ok(new { Message = $"Roles list is empty -> {allRoles.Count}" });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }

        this.Logger.LogInformation($"Role list could not be found!");
        return BadRequest(new
            { Success = false, Message = $"Role list  could not be created!" });
    }


    [Authorize(Roles = "ADMIN")]
    [HttpPut("update/roleById/{id}")]
    public async Task<ActionResult> UpdateRoleById([FromRoute] long id, [FromBody] RoleDataUpdate roleDataUpdate)
    {
        try
        {
            var updatedRole = await this._roleService.UpdateRoleById_ServiceAsync(id, roleDataUpdate);

            if (updatedRole is not null)
            {
                this.Logger.LogInformation($"New role was updated -> {roleDataUpdate.name}");
                return Ok(new { Success = true, RoleUpdated = updatedRole });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }


        this.Logger.LogInformation($"The role {roleDataUpdate.name.ToString()} could not be updated!");
        return BadRequest(new { Success = false, Message = $"The role {roleDataUpdate.name} could not be updated!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("delete/roleById/{id}")]
    public async Task<ActionResult> DeleteRoleById([FromRoute] long id)
    {
        var deletedRole = await this._roleService.DeleteRoleById_ServiceAsync(id);
        try
        {
            if (deletedRole is not null)
            {
                this.Logger.LogInformation($"Role {deletedRole.Name} was removed from DB");
                return Ok(new { Success = true, RoleRemoved = deletedRole.Name });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }


        this.Logger.LogInformation($"The role {deletedRole.Name} could not be removed from DB!");
        return BadRequest(new
            { Success = false, Message = $"The role {deletedRole.Name} could not be removed from DB!" });
    }
}