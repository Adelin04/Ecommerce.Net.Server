using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class BasketItemRepository : IBasketItemRepository
{

    private readonly EcommerceContext _context;

    public BasketItemRepository(EcommerceContext context)
    {
        this._context = context;
    }
    public async Task<BasketItems> AddNewBasketItem(BasketItems newBasketItem)
    {
        var newBasketItemCreated = await this._context.BasketItems.AddAsync(newBasketItem);

        if (newBasketItemCreated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync();
            return newBasketItem;
        }
        return null;
    }


    public async Task<List<BasketItems>> GetAllBasketItems()
    {
        return await this._context.BasketItems.ToListAsync();
    }

    public async Task<List<BasketItems>?> GetBasketItemsByUserId(long id) => await this._context.BasketItems.Include(item => item.Id == id).ToListAsync();


    public async Task<BasketItems?> GetBasketItemsById(long id) => await this._context.BasketItems.FirstOrDefaultAsync(basketItem => basketItem.Id == id);
    public async Task<BasketItems> DeleteBasketItemsById(long id)
    {

        var basketItemToRemove = await GetBasketItemsById(id);

        if (basketItemToRemove is not null)
        {
            this._context.BasketItems.Remove(basketItemToRemove);
            await this._context.SaveChangesAsync();
        }
        return basketItemToRemove;
    }
}