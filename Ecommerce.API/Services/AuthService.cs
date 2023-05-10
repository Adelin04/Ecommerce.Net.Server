using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.API.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IBasketRepository basketRepository,
        IConfiguration configuration)
    {
        this._userRepository = userRepository;
        this._roleRepository = roleRepository;
        this._userRoleRepository = userRoleRepository;
        this._basketRepository = basketRepository;
        this._configuration = configuration;
    }


    public async Task<User> Register(UserDataRegister candidateUser)
    {
        string DEFAULT_ROLE = "USER";
        User? newUser = null;

        var defaultRole = await this._roleRepository.GetRoleByNameAsync(DEFAULT_ROLE);
        var existingUser = await this._userRepository.GetUserByEmailAsync(candidateUser.Email);


        if (existingUser is null && defaultRole is not null)
        {
            newUser = new User();
            newUser.FirstName = candidateUser.Firstname;
            newUser.LastName = candidateUser.Lastname;
            newUser.Email = candidateUser.Email;
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(candidateUser.Password);

            var userCreated = await this._userRepository.SaveNewUser(newUser);

            if (userCreated.Id.ToString() is not null)
                await this._userRoleRepository.AddNewUserRole(userCreated.Id, defaultRole.Id);
        }

        return newUser;
    }


    public async Task<List<object>> Login(UserDataLogin userDataLogin)
    {
        string token = null;
        var existingUser = await this._userRepository.GetUserByEmailAsync(userDataLogin.Email);
        var basketExitByUser = await this._basketRepository.GetBasketByUserId(existingUser.Id);

        if (existingUser is not null)
        {
            var matchPassword = BCrypt.Net.BCrypt.Verify(userDataLogin.Password, existingUser.Password);

            if (matchPassword)
                token = GenerateToken(existingUser);
        }

        return new List<object>() { token, basketExitByUser };
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtConfig:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = GetUserClaims(user);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials
        );


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GetUserClaims(User user)
    {
        List<Claim> claimsList = new List<Claim>();


        claimsList.Add((new Claim(ClaimTypes.GivenName, user.FirstName)));
        claimsList.Add(new Claim(ClaimTypes.Surname, user.LastName));
        claimsList.Add(new Claim(ClaimTypes.Email, user.Email));


        foreach (var role in user.Roles)
        {
            claimsList.Add((new Claim(ClaimTypes.Role, role.Role.Name.ToString())));
        }

        return claimsList;
    }
}