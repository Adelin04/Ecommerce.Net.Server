using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface ICategoryProductRepository
{
    Task<CategoryProduct> AddNewCategoryProductAsync(CategoryProduct newCategoryProduct);
    Task<List<CategoryProduct>> GetAllCategoriesProductAsync();
    Task<CategoryProduct> GetCategoryProductByIdAsync(long id);
    Task<CategoryProduct> GetCategoryProductByNameAsync(string nameCategory);
}
