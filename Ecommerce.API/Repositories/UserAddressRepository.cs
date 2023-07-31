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
    public class UserAddressRepository : IUserAddressesRepository
    {

        private readonly EcommerceContext _context;

        public UserAddressRepository(EcommerceContext context)
        {
            this._context = context;
        }

        public async Task<UserAddress?> CheckExistUsersAddress(string state, string city, string street, string zipCode)
            => await this._context.UsersAdresses.FirstOrDefaultAsync(
                address =>
                    address.State == state &&
                    address.City == city &&
                    address.Street == street &&
                    address.ZipCode == zipCode
        );

        public async Task<UserAddress?> CreateNewUserAddress(UserAddress newUserAdress)
        {
            if (newUserAdress is null) return null;

            var newUserAddressCreated = await this._context.UsersAdresses.AddAsync(newUserAdress);

            if (newUserAddressCreated.State == EntityState.Added)
            {
                await this._context.SaveChangesAsync();
                return newUserAdress;
            }

            return null;
        }

        public Task<UserAddress> GetUserAdressById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAddress?> GetUserAdressByUserId(long id) => await this._context.UsersAdresses.FirstOrDefaultAsync(userAddress => userAddress.UserId == id);

        public Task<UserAddress?> RemoveUserAddress(UserAddress userAdress)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress?> RemoveUserAddressById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress?> RemoveUserAddressByUserId(long id)
        {
            throw new NotImplementedException();
        }
    }
}
