using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly EcommerceContext _context;

    public UserRoleRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<UserRole> AddNewUserRole(long idUser, long idRole)
    {
        UserRole newUserRole = new UserRole { };
        newUserRole.UserId = idUser;
        newUserRole.RoleId = idRole;

        var userRoleCreated = await this._context.UserRoles.AddAsync(newUserRole);

        if (userRoleCreated.State == EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            return newUserRole;
        }

        return null;
    }

    public async Task<List<object>> GetAllRolesByUserId(long id)
    {
        var listRoles = await this._context.UserRoles.ToListAsync();

        foreach (var VARIABLE in listRoles)
        {
            Console.WriteLine("-> " + VARIABLE);
        }
        return new List<object>();
    }
}