
namespace Ecommerce.API.Contracts;

public record RequestRegisterBasketItem(long BuyerId, int Quantity, long ProductId);