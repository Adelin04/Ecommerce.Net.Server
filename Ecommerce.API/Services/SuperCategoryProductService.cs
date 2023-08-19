using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class SuperCategoryProductService
{
    private readonly ISuperCategoryProductRepository _superCategoryProductRepository;

    public SuperCategoryProductService(ISuperCategoryProductRepository superCategoryProductRepository)
    {
        this._superCategoryProductRepository = superCategoryProductRepository;
    }

    public async Task<SuperCategoryProduct> AddNewSuperCategoryProduct_ServiceAsync(SuperCategoryProductDataRegister superCategoryProductDataRegister)
    {
        var existSuperCategoryProduct = await this._superCategoryProductRepository.GetSuperCategoryProductByNameAsync(superCategoryProductDataRegister.Name);

        if (existSuperCategoryProduct is null)
        {
            SuperCategoryProduct newSuperCategoryProduct = new()
            {
                Name = superCategoryProductDataRegister.Name
            };

            var newSuperCategoryProductCreated = await this._superCategoryProductRepository.AddNewSuperCategoryProductAsync(newSuperCategoryProduct);

            return newSuperCategoryProductCreated;
        }
        return null!;

    }

    public async Task<List<SuperCategoryProduct?>> GetAllSuperCategoriesProduct_ServiceAsync()
    {
        var allSuperCategories = await this._superCategoryProductRepository.GetAllSuperCategoriesProductAsync();

        return allSuperCategories!;
    }

    public async Task<SuperCategoryProduct?> DeleteSuperCategoryProductByName_ServiceAsync(string nameSuperCategory)
    {
        var removedSuperCategoryProduct = await this._superCategoryProductRepository.DeleteSuperCategoryProductByNameAsync(nameSuperCategory);

        return removedSuperCategoryProduct!;
    }
}