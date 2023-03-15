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

        if (allCategories is not null)
            return allCategories;
        return null;
    }

    public async Task<CategoryProduct> GetCategoryProductByIdAsync(long id)
    {
        var findCategoryProductById =
            await this._context.CategoryProducts.FirstOrDefaultAsync(categoryProduct => categoryProduct.Id == id);

        if (findCategoryProductById is not null)
            return findCategoryProductById;
        return null;
    }

    public async Task<CategoryProduct> GetCategoryProductByNameAsync(string nameCategory)
    {
        var findCategoryProductByName =
            await this._context.CategoryProducts.FirstOrDefaultAsync(categoryProduct =>
                categoryProduct.Name == nameCategory);


        if (findCategoryProductByName is not null)
            return findCategoryProductByName;
        return null;
    }
}