using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly EcommerceContext _context;

    public ProductRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<Product> CreateNewProductAsync(Product newProduct)
    {
        var newProductCreated = await this._context.Products.AddAsync(newProduct);

        if (newProductCreated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            return newProduct;
        }

        return null;
    }

    public async Task<Product?> GetProductByIdAsync(long id)
    {
        return await this._context.Products.Include(product => product.CategoryProduct)
             .Include(product => product.ProductImages).Include(product => product.SizeStocks)
             .ThenInclude(sizeStock => sizeStock.Size).FirstOrDefaultAsync(product => product.Id == id);

    }

    public async Task<List<Product>?> GetAllProductsAsync()
    {
        return await this._context.Products.Include(categoryProduct => categoryProduct.CategoryProduct)
             .Include(product => product.ProductImages).Include(product => product.SizeStocks).ToListAsync();
    }

    public async Task<Product> DeleteProductByIdAsync(long id)
    {
        var foundProductById = await this._context.Products.FirstOrDefaultAsync(product => product.Id == id);
        var removedProductById = this._context.Products.Remove(foundProductById);

        if (removedProductById.State is EntityState.Deleted)
        {
            await this._context.SaveChangesAsync();
            return foundProductById;
        }

        return null;
    }

    public async Task<Product> UpdateProductByIdAsync(long id, ProductDataUpdate productDataUpdate)
    {
        var foundProductById = await this._context.Products.FirstOrDefaultAsync(product => product.Id == id);

        if (foundProductById is not null)
        {
            foundProductById.Name = productDataUpdate.Name;
            foundProductById.Brand = productDataUpdate.Brand;
            // foundProductById.CategoryProduct = productDataUpdate.CategoryProduct;
            foundProductById.Color = productDataUpdate.Color;
            foundProductById.Description = productDataUpdate.Description;
            foundProductById.Price = productDataUpdate.Price;
            foundProductById.Stock = productDataUpdate.Stock;
        }

        await this._context.SaveChangesAsync();

        return foundProductById;
    }
}