using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ProductImagesRepository : IProductImagesRepository
{
    private readonly EcommerceContext _context;

    public ProductImagesRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<ProductImages> CreateNewProductImagesAsync(ProductImages newProductImages)
    {
        var newImagesCreated = await this._context.ProductImages.AddAsync(newProductImages);

        if (newImagesCreated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            // return newImagesCreated.Entity;
            return newProductImages;
        }

        return null;
    }

    public async Task<ProductImages> GetProductImagesByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductImages> GetProductImagesByPathAsync(string path)
    {
        var foundProductImagesByPath =
            await this._context.ProductImages.FirstOrDefaultAsync(productImages => productImages.Path == path);

        if (foundProductImagesByPath is not null)
            return foundProductImagesByPath;
        return null;
    }

    public Task<ProductImages> GetAllProductImages()
    {
        throw new NotImplementedException();
    }

    public Task<ProductImages> DeleteProductImagesById(long id)
    {
        throw new NotImplementedException();
    }
}