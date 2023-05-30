using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;
public interface IBasketRepository
{
    public Task<Basket> AddNewBasket(Basket newBasket);
    public Task<List<Basket>> GetAllBasket();
    public Task<Basket> GetBasketByUserId(long id);
    public Task<Basket> GetBasketById(long id);

    public Task<Basket> UpdateBasketById(long id);
    public Task<Basket> DeletBasketById(long id);
}