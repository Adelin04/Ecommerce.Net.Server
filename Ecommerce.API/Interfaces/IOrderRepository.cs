using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IOrderRepository
{
    public Task<Order> CreateOrderAsync(Order newOrder);
    public Task<Order> GetOrderByIdAsync(long id);
}
