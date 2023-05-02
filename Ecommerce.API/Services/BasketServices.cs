using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.Extensions.ObjectPool;
using NuGet.Protocol;

namespace Ecommerce.API.Services;

public class BasketServices
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUserRepository _userRepository;
    public BasketServices(IBasketRepository basketRepository, IUserRepository userRepository)
    {
        this._basketRepository = basketRepository;
        this._userRepository = userRepository;
    }

    public async Task<Basket> AddNewBasket_ServiceAsync(RequestRegisterBasket requestRegisterBasket)
    {
        // i'm working now here
        Dictionary<object, object> products = new Dictionary<object, object>();
        var existUser = await this._userRepository.GetUserByEmailAsync(requestRegisterBasket.userEmail);


        foreach (var items in requestRegisterBasket.products)
        {
            foreach (var item in items)
            {
                products.Add(item.Key, item.Value);
            }
        }
        // return await this._repository.AddNewBasket(newBasket);
        System.Console.WriteLine("products------> " + products);
        return null;
    }

    public async Task<List<Basket>> GetAllBaskets_ServiceAsync()
    {
        return await this._basketRepository.GetAllBasket();
    }
}