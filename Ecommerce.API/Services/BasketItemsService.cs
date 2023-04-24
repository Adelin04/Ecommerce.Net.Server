using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;

namespace Ecommerce.API.Services;

public class BasketItemsService
{
    private readonly IBasketItemRepository _basketItemRepository;
    public BasketItemsService(IBasketItemRepository basketItemRepository)
    {
        this._basketItemRepository = basketItemRepository;
    }

    public async Task<BasketItems> AddNewBasketItem_ServiceAsync(BasketItems newBasketItem)
    {
        return await this._basketItemRepository.AddNewBasketItem(newBasketItem);
    }

    public async Task<List<BasketItems>> GetAllBasketItems_ServiceAsync()
    {
        return await this._basketItemRepository.GetAllBasketItems();
    }
}