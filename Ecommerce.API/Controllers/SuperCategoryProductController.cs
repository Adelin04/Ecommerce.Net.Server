using Ecommerce.API.Contracts;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class SuperCategoryProductController : ControllerBase
{
    private readonly SuperCategoryProductService _superCategoryProductService;
    private readonly ILogger<CategoryProductController> Logger;

    public SuperCategoryProductController(SuperCategoryProductService superCategoryProductService,
        ILogger<CategoryProductController> logger)
    {
        this._superCategoryProductService = superCategoryProductService;
        this.Logger = logger;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("create/newSuperCategoryProduct")]
    public async Task<ActionResult> CrateNewSuperCategoryProduct(SuperCategoryProductDataRegister superCategoryProductDataRegister)
    {
        try
        {
            var newSuperCategoryProductCreated = await this._superCategoryProductService.AddNewSuperCategoryProduct_ServiceAsync(superCategoryProductDataRegister);

            if (newSuperCategoryProductCreated is not null)
            {
                this.Logger.LogInformation($"New super category product was created -> {newSuperCategoryProductCreated.Name}");
                return Ok(new { Success = true, NewCategoryProductCreated = newSuperCategoryProductCreated });
            }
        }
        catch (Exception exception)
        {
            this.Logger.LogInformation($"Error -> {exception.Message}");
            return BadRequest($"Error -> {exception.Message}");
        }

        return BadRequest($"The super category product '{superCategoryProductDataRegister.Name}' could not be created!");
    }

    [HttpGet("get/allSuperCategoriesProduct")]
    public async Task<ActionResult> GetAllSuperProductCategories()
    {
        try
        {
            var listOfSuperCategories = await this._superCategoryProductService.GetAllSuperCategoriesProduct_ServiceAsync();
            if (listOfSuperCategories is not null)
            {
                this.Logger.LogInformation($"Returned product super category list");
                return Ok(new { Success = true, ListOfSuperCategories = listOfSuperCategories, Count = listOfSuperCategories.Count });
            }

            if (listOfSuperCategories?.Count < 1)
            {
                this.Logger.LogInformation($"Super category list is empty");
                return Ok(new { Message = $"Super category list is empty -> {listOfSuperCategories.Count} items" });
            }
        }
        catch (Exception exception)
        {
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "Super category product list could not be returned!" });
    }

    [HttpDelete("delete/superCategoryProductByName/{categoryName}")]
    public async Task<ActionResult> DeleteSuperCategoryProductByName([FromRoute] string superCategoryName)
    {

        try
        {
            var removedSuperCategoryName = await this._superCategoryProductService.DeleteSuperCategoryProductByName_ServiceAsync(superCategoryName);

            if (removedSuperCategoryName is not null)
            {
                this.Logger.LogInformation($"Super category {removedSuperCategoryName.Name} was removed from DB");
                return Ok(new { Success = true, CategoryRemoved = removedSuperCategoryName });
            }
        }
        catch (System.Exception exception)
        {

            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }

        this.Logger.LogInformation($"The super category {superCategoryName} could not be removed from DB!");
        return BadRequest(new { Success = false, Message = $"The super category {superCategoryName} could not be removed from DB!" });
    }
}