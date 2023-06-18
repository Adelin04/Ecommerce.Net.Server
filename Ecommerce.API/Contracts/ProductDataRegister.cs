using Ecommerce.API.Models;

namespace Ecommerce.API.Contracts;

public record ProductDataRegister
(
    string Name,
    string Brand,
    string Color,
    string Description,
    double Price,
    string ProductCode,
    string Size,
    long stock,
    string CategoryName,
    List<IFormFile> Files
);