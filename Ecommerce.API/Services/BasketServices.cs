using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
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

        var existUser = await this._userRepository.GetUserByEmailAsync(requestRegisterBasket.userEmail);

        foreach (var product in requestRegisterBasket.products)
        {
            // System.Console.WriteLine(product["productId"]);
            // System.Console.WriteLine("\n");
            // System.Console.WriteLine("quantitySize --------------> " + product["quantitySize"]);

                System.Console.WriteLine(product[0]);
            // foreach (var item in product)
            // {
            // }

            // await this._basketItemRepository.AddNewBasketItem(new BasketItems()
            // { ProductId = (long)products["productId"], Quantity = products["quantitySize"] });

            System.Console.WriteLine("next obj \n");
        }



        // if (existUser is not null)
        // {
        // }

     
        return null;
    }

    public async Task<List<Basket>> GetAllBaskets_ServiceAsync()
    {
        return await this._basketRepository.GetAllBasket();
    }
}