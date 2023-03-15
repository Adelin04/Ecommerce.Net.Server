namespace Ecommerce.API.Contracts;

public record ProductImagesDataRegister(
    string Path,
    long FK_ProductId
);