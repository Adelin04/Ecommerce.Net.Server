using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class CategoryProductRepository : ICategoryProductRepository
{
    private readonly EcommerceContext _context;

    public CategoryProductRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<CategoryProduct> AddNewCategoryProductAsync(CategoryProduct newCategoryProduct)
    {
        var newCategoryProductCreated = await this._context.CategoryProducts.AddAsync(newCategoryProduct);

        if (newCategoryProductCreated.State == EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            return newCategoryProduct;
        }

        return null;
    }

    public Task<List<CategoryProduct>> GetAllCategoriesProductAsync()
    {
        var allCategories = this._context.CategoryProducts.ToListAsync();

        return allCategories;
    }

    public async Task<CategoryProduct> GetCategoryProductByIdAsync(long id)
    {
        var findCategoryProductById =
            await this._context.CategoryProducts.FirstOrDefaultAsync(categoryProduct => categoryProduct.Id == id);

        return findCategoryProductById;
    }

    public async Task<CategoryProduct> GetCategoryProductByNameAsync(string nameCategory)
    {
        var findCategoryProductByName =
            await this._context.CategoryProducts.FirstOrDefaultAsync(categoryProduct =>
                categoryProduct.Name == nameCategory);


        return findCategoryProductByName;
    }

    public async Task<CategoryProduct> DeleteCategoryProductByNameAsync(string nameCategory)
    {
        var existCategoryProductByName = await this._context.CategoryProducts.FirstOrDefaultAsync(categoryProduct => categoryProduct.Name == nameCategory);

        if (existCategoryProductByName is not null)
        {
            var removedCategoryName = this._context.CategoryProducts.Remove(existCategoryProductByName);

            if (removedCategoryName.State is EntityState.Deleted)
            {
                await this._context.SaveChangesAsync();
            }
        }

        return existCategoryProductByName;
    }
}