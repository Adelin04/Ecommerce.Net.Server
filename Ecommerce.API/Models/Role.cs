using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models;

public class Role
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<UserRole> UserRoles { get; set; }
}