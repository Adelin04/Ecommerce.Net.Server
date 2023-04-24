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

    public async Task<Basket> AddNewBasket(Basket newBasket)
    {
        var newBasketCrated = await this._context.Baskets.AddAsync(newBasket);

        if (newBasketCrated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync();
            return newBasket;
        };

        return null;
    }

    public Task<Basket> DeletBasketById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Basket>> GetAllBasket()
    {
        var allBaskets = this._context.Baskets.ToListAsync();

        return allBaskets;
    }

    public Task<Basket> GetBasketById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Basket> UpdateBasketById(long id)
    {
        throw new NotImplementedException();
    }
}