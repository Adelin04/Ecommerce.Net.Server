using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        this._userRepository = userRepository;
        this._roleRepository = roleRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await this._userRepository.GetAllUsersAsync();
    }

    public async Task<User> GetUserById(long id)
    {
        var userById = await this._userRepository.GetUserByIdAsync(id);


        return userById;
    }

    public async Task<User> UpdateUserById(long id, UserDataUpdate userDataUpdate)
    {
        return await this._userRepository.UpdateUserByIdAsync(id, userDataUpdate);
    }

    public async Task<User> DeleteUserById(long id)
    {
        return await this._userRepository.DeleteUserByIdAsync(id);
    }
}