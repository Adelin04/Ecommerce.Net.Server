using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly EcommerceContext _context;

    public BasketRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<Basket?> AddNewBasket(Basket newBasket)
    {
        var newBasketCrated = await this._context.Baskets.AddAsync(newBasket);

        if (newBasketCrated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync();
            return newBasket;
        };

        return null;
    }

    public async Task<Basket?> DeletBasketById(long id)
    {
        var basketToDelete = await this._context.Baskets.FirstOrDefaultAsync(basket => basket.Id == id);

        if (basketToDelete is not null)
        {
            var removedBasketById = this._context.Baskets.Remove(basketToDelete);

            await this._context.SaveChangesAsync();
        }

        return basketToDelete;
    }
    public async Task<List<Basket>> GetAllBasket() => await this._context.Baskets.ToListAsync();

    public async Task<Basket?> GetBasketByUserId(long id) => await this._context.Baskets.Include(item => item.Items).FirstOrDefaultAsync(basket => basket.BuyerId == id);
    public async Task<Basket?> GetBasketById(long id) => await this._context.Baskets.Include(item => item.Items).FirstOrDefaultAsync(basket => basket.Id == id);

    public Task<Basket> UpdateBasketById(long id)
    {
        throw new NotImplementedException();
    }
}