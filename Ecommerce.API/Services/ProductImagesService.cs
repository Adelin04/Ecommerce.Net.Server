using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class ProductImagesService
{
    private readonly IProductImagesRepository _productImagesRepository;

    public ProductImagesService(IProductImagesRepository productImagesRepository)
    {
        this._productImagesRepository = productImagesRepository;
    }

    public async Task<ProductImages> CreateNewProductImages_ServiceAsync(
        ProductImagesDataRegister productImagesDataRegister)
    {
        ProductImages newProductImages = null;

        if (productImagesDataRegister is null)
            return null;
        
        var findProductImages =
            await this._productImagesRepository.GetProductImagesByPathAsync(productImagesDataRegister.Path);

        if (findProductImages is not null)
            return null;

        newProductImages = new ProductImages();
        newProductImages.Path = productImagesDataRegister.Path;
        newProductImages.FK_ProductId = productImagesDataRegister.FK_ProductId;
        

        var newProductImagesCreated =
            await this._productImagesRepository.CreateNewProductImagesAsync(newProductImages);
        
        return newProductImagesCreated;
    }
}