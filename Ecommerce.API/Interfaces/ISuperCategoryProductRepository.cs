using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface ISuperCategoryProductRepository
{
    Task<SuperCategoryProduct> AddNewSuperCategoryProductAsync(SuperCategoryProduct newSuperCategoryProduct);
    Task<List<SuperCategoryProduct>> GetAllSuperCategoriesProductAsync();
    Task<SuperCategoryProduct> GetSuperCategoryProductByIdAsync(long id);
    Task<SuperCategoryProduct> GetSuperCategoryProductByNameAsync(string nameSuperCategory);
    Task<SuperCategoryProduct> DeleteSuperCategoryProductByNameAsync(string nameSuperCategory);
}
