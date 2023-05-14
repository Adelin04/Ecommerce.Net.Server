using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.WebEncoders.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        if (existUser is null) return null;

        var newBasketCreated = await this._basketRepository.AddNewBasket(new Basket() { BuyerId = existUser.Id });

        if (newBasketCreated is null) return null;

        foreach (var product in requestRegisterBasket.products)
        {
            foreach (var quntitySize in (JArray)product["quantitySize"])
            {
                await this._basketItemRepository.AddNewBasketItem(new BasketItems()
                { ProductId = (long)product["productId"], Quantity = (int)quntitySize["quantity"], Size = quntitySize["size"].ToString(), BasketId = newBasketCreated.Id });
            }
        }

        return newBasketCreated;
    }

    public async Task<List<Basket>> GetAllBaskets_ServiceAsync()
    {
        return await this._basketRepository.GetAllBasket();
    }
    public async Task<Basket?> GetBasketByUserEmail_ServiceAsync(string email)
    {
        var userByEmail = await this._userRepository.GetUserByEmailAsync(email);

        System.Console.WriteLine("-------------------- " + userByEmail.Id);
        if (userByEmail is null)
            return null;
        return await this._basketRepository.GetBasketByUserId(userByEmail.Id);
    }
}