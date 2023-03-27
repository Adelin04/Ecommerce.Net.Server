using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IProductImagesRepository
{
    public Task<ProductImages> CreateNewProductImagesAsync(ProductImages newProductImages);
    public Task<ProductImages> GetProductImagesByIdAsync(long id);
    public Task<ProductImages> GetProductImagesByPathAsync(string path);
    public Task<ProductImages> GetAllProductImages();
    public Task<ProductImages> DeleteProductImagesById(long id);
}