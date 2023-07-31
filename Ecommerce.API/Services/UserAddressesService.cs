using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class UserAddressesService
{
    private readonly IUserAddressesRepository _userAddressesRepository;

    public UserAddressesService(IUserAddressesRepository userAddressesRepository)
    {
        this._userAddressesRepository = userAddressesRepository;
    }

    public async Task<UserAddress?> GetUserAddressesByUserId_ServiceAsync(long id)
    {

        var existUserAddress = await this._userAddressesRepository.GetUserAdressByUserId(id);

        return existUserAddress;
    }
}