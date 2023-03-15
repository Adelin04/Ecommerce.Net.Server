using Ecommerce.API.Contracts;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IAuthRepository
{
    Task<User> CreateUserAsync(User newUser);
}