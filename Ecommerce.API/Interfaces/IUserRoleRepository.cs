using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IUserRoleRepository
{
    Task<UserRole> AddNewUserRole(long idUser, long idRole);
    Task<List<object>> GetAllRolesByUserId(long id);
}