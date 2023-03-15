namespace Ecommerce.API.Contracts;

public record UserDataRegister(string Lastname, string Firstname, string Email, string Password,
    string ProfileImagePath);