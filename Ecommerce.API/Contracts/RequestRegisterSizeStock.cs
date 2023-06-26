namespace Ecommerce.API.Contracts;

public record RequestRegisterSizeStock(
    long idProduct,
    List<Dictionary<string, object>> listOfNewSizeStock
);