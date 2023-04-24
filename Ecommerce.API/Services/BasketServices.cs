using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class BasketServices
{
    private readonly IBasketRepository _repository;
    public BasketServices(IBasketRepository repository)
    {
        this._repository = repository;
    }

    public async Task<Basket> AddNewBasket_ServiceAsync(Basket newBasket)
    {
        return await this._repository.AddNewBasket(newBasket);
    }

    public async Task<List<Basket>> GetAllBaskets_ServiceAsync()
    {
        return await this._repository.GetAllBasket();
    }
}