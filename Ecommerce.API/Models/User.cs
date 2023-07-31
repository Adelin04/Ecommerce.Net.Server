using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Models;

public class User
{

    public User()
    {
        this.Roles = new HashSet<UserRole>();
        this.UserAddresses = new HashSet<UserAddress>();
    }

    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public virtual ICollection<UserRole> Roles { get; set; }
    public virtual ICollection<UserAddress> UserAddresses { get; set; }

    [JsonIgnore]
    public string Password { get; set; } = string.Empty;
    public string? ProfileImagePath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


}