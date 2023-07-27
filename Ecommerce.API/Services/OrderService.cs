using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Newtonsoft.Json.Linq;

namespace Ecommerce.API.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAddressCustomerRepository _addressCustomerRepository;


    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IAddressCustomerRepository addressCustomerRepository)
    {
        this._orderRepository = orderRepository;
        this._userRepository = userRepository;
        this._addressCustomerRepository = addressCustomerRepository;
    }

    public async Task<Order> CreateNewOrder(RequestRegisterOrder requestRegisterOrder)
    {
        var objectDestructured = JObject.Parse(requestRegisterOrder.DataAddress.ToString());
        var userExist = await this._userRepository.GetUserByEmailAsync(objectDestructured["email"].ToString());
        var addressExist = await this._addressCustomerRepository.GetAddressCustomerByUserIdAsync(userExist.Id);


        if (addressExist is null)
        {
            AddressCustomer newAddressCustomer = new AddressCustomer()
            { City = objectDestructured["city"].ToString(), State = objectDestructured["country"].ToString(), Notes = objectDestructured["notes"].ToString(), ZipCode = objectDestructured["zipCode"].ToString(), User = userExist is not null ? userExist : null };

            var newAddressCustomerCreated = await this._addressCustomerRepository.CreateAddressCustomerAsync(newAddressCustomer);
        }

        Order newOrder = new() { User = userExist, AddressCustomerId = userExist.Id };
        var newOrderCreated = await this._orderRepository.CreateOrderAsync(newOrder);


        return newOrderCreated;
    }
}