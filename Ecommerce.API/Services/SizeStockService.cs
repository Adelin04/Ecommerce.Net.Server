using Ecommerce.API.Models;
using Ecommerce.API.Repositories;

namespace Ecommerce.API.Services;

public class SizeStockService
{
    private readonly SizeStockRepository _sizeStockRepository;
    private readonly ProductRepository _productRepository;
    private readonly SizeRepository _sizeRepository;

    public SizeStockService(SizeStockRepository sizeStockRepository, ProductRepository productRepository, SizeRepository sizeRepository)
    {
        this._sizeStockRepository = sizeStockRepository;
        this._productRepository = productRepository;
        this._sizeRepository = sizeRepository;
    }

    public async Task<SizeStock?> AddNewSizeAndStockExistProduct_ServiceAsync(long id, long stock, string size)
    {
        var existProduct = await this._productRepository.GetProductByIdAsync(id);
        var existSize = await this._sizeRepository.GetSizeByNameAsync(size);

        if (existProduct is null) return null;
        if (existSize is null) return null;

        var newSizeAndStock = new SizeStock() { Stock = stock, FK_ProductId = existProduct.Id, FK_SizeId = existSize.Id };
        var newSizeAndSrockAdded = await this._sizeStockRepository.RegisterNewSizeStockAsync(newSizeAndStock);

        if (newSizeAndSrockAdded is null) return null;

        return newSizeAndStock;
    }
}