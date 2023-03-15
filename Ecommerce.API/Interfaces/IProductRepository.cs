using Ecommerce.API.Contracts;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IProductRepository
{
    public Task<Product> CreateNewProductAsync(Product newProduct);

    public Task<Product> GetProductByIdAsync(long id);

    public Task<List<Product>> GetAllProductsAsync();

    public Task<Product> UpdateProductByIdAsync(long id, ProductDataUpdate productDataUpdate);

    public Task<Product> DeleteProductByIdAsync(long id);
}