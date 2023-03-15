using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;

namespace Ecommerce.API.Services;

public class UserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        this._userRoleRepository = userRoleRepository;
    }

    public async Task<UserRole> AddNewUserRole(long idUser, long idRole)
    {
        var newUserRoleCreated = await this._userRoleRepository.AddNewUserRole(idUser, idRole);
        return newUserRoleCreated;
    }
}