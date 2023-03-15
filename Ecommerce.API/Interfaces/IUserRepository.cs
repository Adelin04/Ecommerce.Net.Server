using Ecommerce.API.Contracts;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(long id);
    Task<User> UpdateUserByIdAsync(long id, UserDataUpdate userDataUpdate);
    Task<User> DeleteUserByIdAsync(long id);
}