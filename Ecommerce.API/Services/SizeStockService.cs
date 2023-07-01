using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;

namespace Ecommerce.API.Services;

public class SizeStockService
{
    private readonly ISizeStockRepository _sizeStockRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISizeRepository _sizeRepository;

    public SizeStockService(ISizeStockRepository sizeStockRepository, IProductRepository productRepository, ISizeRepository sizeRepository)
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
    public async Task<List<Dictionary<string, object>>> AddListNewSizeAndStockExistProduct_ServiceAsync(RequestRegisterSizeStock requestRegisterSizeStock)
    {
        List<SizeStock> TMP_listOfSizeAndStock = new();

        foreach (var item in requestRegisterSizeStock.listOfNewSizeStock)
        {
            var existProduct = await this._productRepository.GetProductByIdAsync(requestRegisterSizeStock.idProduct);
            var existSize = await this._sizeRepository.GetSizeByNameAsync((string)item["size"]);

            if (existProduct is null) return null;
            if (existSize is null) return null;

            var newSizeAndStock = new SizeStock() { Stock = (long)item["stock"], FK_ProductId = existProduct.Id, FK_SizeId = existSize.Id };
            TMP_listOfSizeAndStock.Add(newSizeAndStock);
        }
        var newSizeAndSrockAdded = await this._sizeStockRepository.RegisterListOfNewSizeStockAsync(TMP_listOfSizeAndStock);

        if (newSizeAndSrockAdded is null) return null;

        return requestRegisterSizeStock.listOfNewSizeStock;
    }
}