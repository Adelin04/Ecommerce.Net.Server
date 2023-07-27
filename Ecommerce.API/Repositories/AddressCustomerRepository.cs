using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class AddressCustomerRepository : IAddressCustomerRepository
    {

        private readonly EcommerceContext _context;

        public AddressCustomerRepository(EcommerceContext context)
        {
            this._context = context;
        }

        public async Task<AddressCustomer?> CreateAddressCustomerAsync(AddressCustomer newAddressCustomer)
        {
            var createdNewAddressCustomer = await this._context.AddAsync(newAddressCustomer);

            if (createdNewAddressCustomer.State is EntityState.Added)
            {
                await this._context.SaveChangesAsync();
                return newAddressCustomer;
            }
            return null;
        }

        public async Task<AddressCustomer?> GetAddressCustomerByUserIdAsync(long id)=> await this._context.AddressCustomers.FirstOrDefaultAsync(address=>address.UserId == id);

    }
}