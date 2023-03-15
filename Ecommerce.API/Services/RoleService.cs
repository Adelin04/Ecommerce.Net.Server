using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        this._roleRepository = roleRepository;
    }


    public async Task<Role> CreateNewRole_ServiceAsync(RoleDataCreateNewRole dataCreateNewRole)
    {
        Role newRole = null;
        var existingRole = await this._roleRepository.GetRoleByNameAsync(dataCreateNewRole.nameRole);

        if (existingRole is null)
        {
            newRole = new Role();
            newRole.Name = dataCreateNewRole.nameRole;
            await this._roleRepository.CreateNewRoleAsync(newRole);
            return newRole;
        }

        return newRole;
    }

    public async Task<List<Role>> GetAllRoles_ServiceAsync()
    {
        var allRoles = await this._roleRepository.GetAllRolesAsync();

        if (allRoles is not null)
            return allRoles;
        return null;
    }

    public async Task<Role> GetRoleById_ServiceAsync(long id)
    {
        var roleById = await this._roleRepository.GetRoleByIdAsync(id);

        if (roleById is not null)
            return roleById;
        return null;
    }

    public async Task<Role> UpdateRoleById_ServiceAsync(long id, RoleDataUpdate roleDataUpdate)
    {
        return await this._roleRepository.UpdateRoleByIdAsync(id, roleDataUpdate);
    }

    public async Task<Role> DeleteRoleById_ServiceAsync(long id)
    {
        return await this._roleRepository.DeleteRoleByIdAsync(id);
    }
}