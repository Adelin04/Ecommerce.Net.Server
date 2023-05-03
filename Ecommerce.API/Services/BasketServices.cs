using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;

namespace Ecommerce.API.Services;

public class BasketServices
{
    private readonly IBasketRepository _basketRepository;
    private readonly IBasketItemRepository _basketItemRepository;
    private readonly IUserRepository _userRepository;
    public BasketServices(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IUserRepository userRepository)
    {
        this._basketRepository = basketRepository;
        this._basketItemRepository = basketItemRepository;
        this._userRepository = userRepository;
    }

    public async Task<Basket> AddNewBasket_ServiceAsync(RequestRegisterBasket requestRegisterBasket)
    {
        // i'm working now here
        List<BasketItems> products = new();
        Dictionary<object, object> listProd = new();

        var existUser = await this._userRepository.GetUserByEmailAsync(requestRegisterBasket.userEmail) is User;

        if (existUser)
        {
            foreach (var items in requestRegisterBasket.products)
            {
                foreach (var item in items)
                {

                    System.Console.WriteLine(item.Value);

                }
            }
        }

        // return await this._repository.AddNewBasket(newBasket);
        return null;
    }

    public async Task<List<Basket>> GetAllBaskets_ServiceAsync()
    {
        return await this._basketRepository.GetAllBasket();
    }
}