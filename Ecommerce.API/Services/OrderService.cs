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
    private readonly IUsersAddressesRepository _usersAddressesRepository;


    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IAddressCustomerRepository addressCustomerRepository, IUsersAddressesRepository usersAddressesRepository)
    {
        this._orderRepository = orderRepository;
        this._userRepository = userRepository;
        this._addressCustomerRepository = addressCustomerRepository;
        this._usersAddressesRepository = usersAddressesRepository;
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
            Street = objectDestructured["Street"].ToString(),
            Notes = objectDestructured["notes"].ToString(),
            ZipCode = objectDestructured["zipCode"].ToString(),
            Phone = objectDestructured["phone"].ToString(),
            User = userExist is not null ? userExist : null
        };

        // var newAddressCustomerCreated = await this._addressCustomerRepository.CreateAddressCustomerAsync(newAddressCustomer);

        // Order newOrder = new() { User = userExist is not null ? userExist : null, AddressCustomerId = newAddressCustomer.id };
        // var newOrderCreated = await this._orderRepository.CreateOrderAsync(newOrder);

        // if ((Boolean)objectDestructured["saveAddress"] is true)
        // {
        //     UserAddress newUserAddress = new()
        //     {
        //         Email = objectDestructured["email"].ToString(),
        //         City = objectDestructured["city"].ToString(),
        //         State = objectDestructured["country"].ToString(),
        //         Street = objectDestructured["Street"].ToString(),
        //         ZipCode = objectDestructured["zipCode"].ToString(),
        //         Phone = objectDestructured["phone"].ToString(),
        //         User = userExist is not null ? userExist : null
        //     };
        //     await this._usersAddressesRepository.CreateNewUserAddress(newUserAddress);
        // }


        // if (userExist is not null)
        // {

        //     if ((Boolean)objectDestructured["saveAddress"] is true)
        //     {
        //         var addressExist = await this._addressCustomerRepository.GetAddressCustomerByStateCityZipcodeAsync(
        //             objectDestructured["country"].ToString(),
        //             objectDestructured["city"].ToString(),
        //             objectDestructured["zipCode"].ToString()
        //         );

        //         if (addressExist is null)
        //         {
        //             AddressCustomer newAddressCustomer = new AddressCustomer()
        //             {
        //                 LastName = objectDestructured["firstName"].ToString(),
        //                 FirstName = objectDestructured["firstName"].ToString(),
        //                 Email = objectDestructured["email"].ToString(),
        //                 City = objectDestructured["city"].ToString(),
        //                 State = objectDestructured["country"].ToString(),
        //                 Street = objectDestructured["Street"].ToString(),
        //                 Notes = objectDestructured["notes"].ToString(),
        //                 ZipCode = objectDestructured["zipCode"].ToString(),
        //                 Phone = objectDestructured["phone"].ToString(),
        //                 User = userExist is not null ? userExist : null
        //             };

        //             var newAddressCustomerCreated = await this._addressCustomerRepository.CreateAddressCustomerAsync(newAddressCustomer);
        //         }
        //     }

        //     Order newOrder = new() { User = userExist, AddressCustomerId = userExist.Id };
        //     var newOrderCreated = await this._orderRepository.CreateOrderAsync(newOrder);


        //     return newOrderCreated;
        // }
        // else
        // {
        //     AddressCustomer newAddressCustomer = new AddressCustomer()
        //     {
        //         LastName = objectDestructured["firstName"].ToString(),
        //         FirstName = objectDestructured["firstName"].ToString(),
        //         Email = objectDestructured["email"].ToString(),
        //         City = objectDestructured["city"].ToString(),
        //         State = objectDestructured["country"].ToString(),
        //         Notes = objectDestructured["notes"].ToString(),
        //         ZipCode = objectDestructured["zipCode"].ToString(),
        //         Phone = objectDestructured["phone"].ToString(),
        //         User = null
        //     };
        //     var newAddressCustomerCreated = await this._addressCustomerRepository.CreateAddressCustomerAsync(newAddressCustomer);

        //     Order newOrder = new() { AddressCustomerId = newAddressCustomerCreated.id };
        //     var newOrderCreated = await this._orderRepository.CreateOrderAsync(newOrder);
        // }

        System.Console.WriteLine("-------------------newAddressCustomer--------------------" + newAddressCustomer);

        return null;
    }
}