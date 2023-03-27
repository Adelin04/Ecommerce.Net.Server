namespace Ecommerce.API.Models;

public class ProductImages
{
    public long Id;
    public string Path { get; set; }
    public virtual Product Product { get; set; }

    public long FK_ProductId;
}