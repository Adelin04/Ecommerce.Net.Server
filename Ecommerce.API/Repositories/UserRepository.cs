using Ecommerce.API.Contracts;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EcommerceContext _context;

    public UserRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<User> SaveNewUser(User newUser)
    {
        var newUserCreated = await this._context.Users.AddAsync(newUser);

        if (newUserCreated.State == EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            return newUser;
        }

        return null;
    }


    public async Task<List<User>> GetAllUsersAsync()
    {
        var usersList = await this._context.Users
            .Include(user => user.Roles)
            .Include(address => address.UserAddresses)
            .ToListAsync();

        // var usersList = await this._context.Users
        //     .Join(this._context.UserRoles, user => user.Id, userRole => userRole.UserId, (u, ur) => new { u, ur })
        //     .Join(this._context.Role, userRole => userRole.ur.RoleId, role => role.Id, (ur, r) => new { ur, r })
        //     .ToListAsync();

        return usersList;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var foundUserByEmail = await this._context.Users
            .Include(user => user.Roles)
            .ThenInclude(role => role.Role)
            .Include(address => address.UserAddresses)
            .FirstOrDefaultAsync(user => user.Email == email);
        // var userAddress = await this._context.Users.Include(user => user.UserAddresses)
        //     .FirstOrDefaultAsync(user => user.Email == email);

        return foundUserByEmail;
    }

    public async Task<User?> GetUserByIdAsync(long id)
    {
        var foundUserById = await this._context.Users
            .Include(user => user.Roles)
            .ThenInclude(role => role.Role)
            .Include(address => address.UserAddresses)
            .FirstOrDefaultAsync(user => user.Id == id);

        return foundUserById;
    }

    public async Task<User> UpdateUserByIdAsync(long id, UserDataUpdate userDataUpdate)
    {
        var foundUserById = await this._context.Users.FirstOrDefaultAsync(user => user.Id == id);

        foundUserById.FirstName = userDataUpdate.FirstName;
        foundUserById.LastName = userDataUpdate.LastName;
        foundUserById.ProfileImagePath = userDataUpdate.ProfileImagePath;
        foundUserById.Email = userDataUpdate.Email;

        await this._context.SaveChangesAsync();

        return foundUserById;
    }

    public async Task<User> DeleteUserByIdAsync(long id)
    {
        var userByIdToDelete = await this._context.Users.FirstOrDefaultAsync(user => user.Id == id);
        var removedUserById = this._context.Users.Remove(userByIdToDelete);

        if (removedUserById.State == EntityState.Deleted)
        {
            await this._context.SaveChangesAsync();
            return userByIdToDelete;
        }

        return null;
    }
}