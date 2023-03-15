using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly EcommerceContext _context;

    public AuthRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<User> CreateUserAsync(User newUser)
    {
        var newUserCreated = await this._context.Users.AddAsync(newUser);

        if (newUserCreated.State == EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            return newUser;
        }

        return null;
    }
}