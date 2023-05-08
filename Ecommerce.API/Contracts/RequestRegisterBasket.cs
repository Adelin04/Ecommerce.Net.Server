using Ecommerce.API.Models;

namespace Ecommerce.API.Contracts;

public record RequestRegisterBasket(
    string userEmail,
    List<Dictionary<string, object>> products
);