using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;

namespace Ecommerce.API.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAddressCustomerRepository _addressCustomerRepository;
    private readonly IUserAddressesRepository _userAddressesRepository;


    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IAddressCustomerRepository addressCustomerRepository, IUserAddressesRepository userAddressesRepository)
    {
        this._orderRepository = orderRepository;
        this._userRepository = userRepository;
        this._addressCustomerRepository = addressCustomerRepository;
        this._userAddressesRepository = userAddressesRepository;
    }

    public async Task<object?> CreateNewOrder(RequestRegisterOrder requestRegisterOrder)
    {
        if (requestRegisterOrder is null)
            return null;

        var objectDestructured = JObject.Parse(requestRegisterOrder.DataAddress.ToString());
        var userExist = await this._userRepository.GetUserByEmailAsync(objectDestructured["email"].ToString());

        AddressCustomer newAddressCustomer = new AddressCustomer()
        {
            LastName = objectDestructured["firstName"].ToString(),
            FirstName = objectDestructured["lastName"].ToString(),
            Email = objectDestructured["email"].ToString(),
            City = objectDestructured["city"].ToString(),
            State = objectDestructured["state"].ToString(),
            Street = objectDestructured["street"].ToString(),
            Notes = objectDestructured["notes"].ToString(),
            ZipCode = objectDestructured["zipCode"].ToString(),
            Phone = objectDestructured["phone"].ToString(),
            // User = userExist is not null ? userExist : null,
        };
        var newAddressCustomerCreated = await this._addressCustomerRepository.CreateAddressCustomerAsync(newAddressCustomer);

        Order newOrder = new() { User = userExist is not null ? userExist : null, AddressCustomerId = newAddressCustomer.id };
        var newOrderCreated = await this._orderRepository.CreateOrderAsync(newOrder);

        if (userExist is not null && (Boolean)objectDestructured["saveAddress"] is true)
        {
            var addressExist = await this._userAddressesRepository.CheckExistUsersAddress(
                   objectDestructured["state"].ToString(),
                   objectDestructured["street"].ToString(),
                   objectDestructured["city"].ToString(),
                   objectDestructured["zipCode"].ToString()
               );

            if (addressExist is null)
            {
                UserAddress newUserAddress = new()
                {
                    Email = objectDestructured["email"].ToString(),
                    State = objectDestructured["state"].ToString(),
                    City = objectDestructured["city"].ToString(),
                    Street = objectDestructured["street"].ToString(),
                    ZipCode = objectDestructured["zipCode"].ToString(),
                    Phone = objectDestructured["phone"].ToString(),
                    UserId = userExist.Id,
                };

                var userAddressCreated = await this._userAddressesRepository.CreateNewUserAddress(newUserAddress);
            }
        }


        return newOrderCreated;
    }
}