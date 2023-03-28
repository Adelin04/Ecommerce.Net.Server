using Ecommerce.API.Contracts;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class CategoryProductController : ControllerBase
{
    private readonly CategoryProductService _categoryProductService;
    private readonly ILogger<CategoryProductController> Logger;

    public CategoryProductController(CategoryProductService categoryProductService,
        ILogger<CategoryProductController> logger)
    {
        this._categoryProductService = categoryProductService;
        this.Logger = logger;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("create/newCategoryProduct")]
    public async Task<ActionResult> CrateNewCategoryProduct(CategoryProductDataRegister categoryProductDataRegister)
    {
        try
        {
            var newCategoryProductCreated =
                await this._categoryProductService.AddNewCategoryProduct_ServiceAsync(categoryProductDataRegister);

            if (newCategoryProductCreated is not null)
            {
                this.Logger.LogInformation($"New category product was created -> {newCategoryProductCreated.Name}");
                return Ok(new { Success = true, NewCategoryProductCreated = newCategoryProductCreated });
            }
        }
        catch (Exception exception)
        {
            this.Logger.LogInformation($"Error -> {exception.Message}");
            return BadRequest($"Error -> {exception.Message}");
        }

        return BadRequest($"The product {categoryProductDataRegister.Name} could not be created!");
    }

    [HttpGet("get/allCategoriesProduct")]
    public async Task<ActionResult> GetAllProductCategories()
    {
        try
        {
            var listOfCategories = await this._categoryProductService.GetAllCategoriesProduct_ServiceAsync();
            if (listOfCategories is not null)
            {
                this.Logger.LogInformation($"Returned product category list");
                return Ok(new { Success = true, ListOfCategories = listOfCategories, Count = listOfCategories.Count });
            }

            if (listOfCategories.Count < 1)
            {
                this.Logger.LogInformation($"Returned product category list");
                return Ok(new { Message = $"Products list is empty -> {listOfCategories.Count}" });
            }
        }
        catch (Exception exception)
        {
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "Product category list could not be returned!" });
    }

    [HttpDelete("delete/categoryByName/{name}")]
    public async Task<ActionResult> DeleteCategoryProductByName([FromRoute] string name){

        // System.Console.WriteLine("name" + name);
        var removedProductCategory = await this._categoryProductService.DeleteCategoryProductByName(name)
        
        try
        {
            if(removedCategoryProduct is not null){
                this.Logger.LogInformation($"Category {removedCategoryProduct.Name} was removed from DB");
                return Ok(new { Success = true, CategoryRemoved = removedCategoryProduct });
            }
        }
        catch (System.Exception)
        {
          
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }

        this.Logger.LogInformation($"The category {removedProduct.Name} could not be removed from DB!");
        return BadRequest(new { Success = false, Message = $"The category {removedProduct.Name} could not be removed from DB!" });
    }
}