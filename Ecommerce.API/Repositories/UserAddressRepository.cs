using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Repositories
{
    public class UserAddressRepository : IUsersAddressesRepository
    {
        public Task<UserAddress> CreateNewUserAddress(UserAddress newUserAdress)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> GetUserAdressById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> GetUserAdressByUserId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> RemoveUserAddress(UserAddress userAdress)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> RemoveUserAddressById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> RemoveUserAddressByUserId(long id)
        {
            throw new NotImplementedException();
        }
    }
}