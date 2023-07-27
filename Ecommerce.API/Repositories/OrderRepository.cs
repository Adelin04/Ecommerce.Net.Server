using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ecommerce.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceContext _context;

    public OrderRepository(EcommerceContext context)
    {
        this._context = context;
    }


    public async Task<Order?> CreateOrderAsync(Order newOrder)
    {
        var newOrderCreated = await this._context.AddAsync(newOrder);

        if (newOrderCreated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync();
            return newOrder;
        }
        return null;
    }

    public Task<Order?> GetOrderByIdAsync(long id) {
        return null;
     }

}