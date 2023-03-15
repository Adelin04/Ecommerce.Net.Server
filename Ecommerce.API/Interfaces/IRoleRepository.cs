using Ecommerce.API.Contracts;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IRoleRepository
{
    Task<Role> CreateNewRoleAsync(Role newRole);
    Task<List<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(long id);
    Task<Role> GetRoleByNameAsync(string name);
    Task<Role> UpdateRoleByIdAsync(long id, RoleDataUpdate roleDataUpdate);
    Task<Role> DeleteRoleByIdAsync(long id);
}
