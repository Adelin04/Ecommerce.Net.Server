using Ecommerce.API.Models;

namespace Ecommerce.API.Contracts;

public record ProductDataRegister
(
    string Name,
    string Brand,
    string Color,
    string Description,
    double Price,
    List<SizeStockDataRegister> Sizes,
    string CategoryName,
    List<IFormFile> Files
);