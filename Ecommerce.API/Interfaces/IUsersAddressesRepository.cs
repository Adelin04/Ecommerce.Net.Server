using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;
public interface IUserAddressesRepository
{
    public Task<UserAddress> CreateNewUserAddress(UserAddress newUserAdress);
    public Task<UserAddress> GetUserAdressById(long id);
    public Task<UserAddress> GetUserAdressByUserId(long id);
    public Task<UserAddress> CheckExistUsersAddress(string state, string city, string street, string zipCode);
    public Task<UserAddress> RemoveUserAddressById(long id);
    public Task<UserAddress> RemoveUserAddressByUserId(long id);
    public Task<UserAddress> RemoveUserAddress(UserAddress userAdress);
}