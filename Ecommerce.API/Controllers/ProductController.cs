using Ecommerce.API.Contracts;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualBasic;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly ILogger<ProductController> Logger;

    public ProductController(ProductService productService, ILogger<ProductController> logger)
    {
        this._productService = productService;
        this.Logger = logger;
    }

    // [Authorize(Roles = "ADMIN")]
    [Consumes("multipart/form-data")]
    [HttpPost("create/newProduct")]
    public async Task<ActionResult> CreateNewProduct([FromForm] ProductDataRegister productDataRegister)
    {
        try
        {
            var newProductCreated = await this._productService.CreateNewProduct_ServiceAsync(productDataRegister);

            if (newProductCreated is not null)
                return Ok(new { Success = true,  NewProductCreated = newProductCreated });
        }
        catch (Exception exception)
        {
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new {Success = false, Error = exception.Message });
        }

        return BadRequest(new
            { Success = false, Message = $"The product {productDataRegister.Name} could not be created!" });
    }

    [HttpGet("get/allProducts")]
    public async Task<ActionResult> GetAllProducts()
    {
        try
        {
            var listOfProducts = await this._productService.GetAllProducts_ServiceAsync();
            if (listOfProducts is not null)
            {
                this.Logger.LogInformation($"Returned products list");
                return Ok(new { Success = true, Products = listOfProducts, Count = listOfProducts.Count });
            }

            if (listOfProducts.Count < 1)
            {
                this.Logger.LogInformation($"Returned products list");
                return Ok(new { Message = $"Products list is empty -> {listOfProducts.Count}" });
            }
        }
        catch (Exception exception)
        {
            this.Logger.LogInformation("Error -> " + exception.Message);
            return BadRequest(new { Error = exception.Message });
        }

        return BadRequest(new { Success = false, Message = "Products list could not be returned!" });
    }

    [HttpGet("get/productById/{id}")]
    public async Task<ActionResult> GetProductById([FromRoute] long id)
    {
        try
        {
            var productById = await this._productService.GetProductById_ServiceAsync(id);

            if (productById is not null)
                return Ok(new { Success = true, ProductById = productById });
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation("Error -> " + exception.Message);
        }

        return BadRequest(new { Success = false, Message = "No product found!" });
    }


    [Authorize(Roles = "ADMIN")]
    [HttpPut("update/productById/{id}")]
    public async Task<ActionResult> UpdateProductById([FromRoute] long id,
        [FromBody] ProductDataUpdate productDataUpdate)
    {
        var updatedProduct = await this._productService.UpdateProductById_ServiceAsync(id, productDataUpdate);
        try
        {
            if (productDataUpdate is not null)
            {
                this.Logger.LogInformation($"New product was updated -> {productDataUpdate.Name}");
                return Ok(new { Success = true, ProductUpdated = productDataUpdate });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }


        this.Logger.LogInformation($"The product {productDataUpdate.Name} could not be updated!");
        return BadRequest(new
            { Success = false, Message = $"The product {productDataUpdate.Name} could not be updated!" });
    }

    // [Authorize(Roles = "ADMIN")]
    [HttpDelete("delete/productById/{id}")]
    public async Task<ActionResult> DeleteProductById([FromRoute] long id)
    {
        Console.WriteLine(id);
        var removedProduct = await this._productService.DeleteProductById_ServiceAsync(id);
        try
        {
            if (removedProduct is not null)
            {
                this.Logger.LogInformation($"Product {removedProduct.Name} was removed from DB");
                return Ok(new { Success = true, ProductRemoved = removedProduct });
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error -> " + exception.Message);
            this.Logger.LogInformation(exception.Message.ToString());
        }


        this.Logger.LogInformation($"The product {removedProduct.Name} could not be removed from DB!");
        return BadRequest(new
            { Success = false, Message = $"The product {removedProduct.Name} could not be removed from DB!" });
    }
}