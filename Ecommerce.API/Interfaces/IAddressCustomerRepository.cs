using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

    public interface IAddressCustomerRepository
    {
        Task<AddressCustomer> CreateAddressCustomerAsync(AddressCustomer newAddressCustomer);        
        Task<AddressCustomer> GetAddressCustomerByUserIdAsync(long id);        
    }