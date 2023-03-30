using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;
using NuGet.Protocol;

namespace Ecommerce.API.Services;

public class CategoryProductService
{
    private readonly ICategoryProductRepository _categoryProductRepository;

    public CategoryProductService(ICategoryProductRepository categoryProductRepository)
    {
        this._categoryProductRepository = categoryProductRepository;
    }

    public async Task<CategoryProduct> AddNewCategoryProduct_ServiceAsync(CategoryProductDataRegister categoryProductDataRegister)
    {
        var existCategoryProduct = await this._categoryProductRepository.GetCategoryProductByNameAsync(categoryProductDataRegister.Name);
        CategoryProduct newCategoryProduct = null;

        if (existCategoryProduct is null)
        {
            newCategoryProduct = new CategoryProduct();
            newCategoryProduct.Name = categoryProductDataRegister.Name;

            var newCategoryProductCreated = await this._categoryProductRepository.AddNewCategoryProductAsync(newCategoryProduct);
        }

        return newCategoryProduct;
    }

    public async Task<List<CategoryProduct>> GetAllCategoriesProduct_ServiceAsync()
    {
        var allCategories = await this._categoryProductRepository.GetAllCategoriesProductAsync();

        return allCategories;
    }

    public async Task<CategoryProduct> DeleteCategoryProductByName_ServiceAsync(string nameCategory)
    {
        var removedCategoryProduct = await this._categoryProductRepository.DeleteCategoryProductByNameAsync(nameCategory);

        return removedCategoryProduct;
    }
}