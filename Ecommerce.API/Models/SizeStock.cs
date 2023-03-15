namespace Ecommerce.API.Models;

public class SizeStock
{
    public long Id;
    public long Stock { get; set; }

    public long FK_ProductId;
    public virtual Product Product { get; set; }

    public long FK_SizeId;
    public virtual Size Size { get; set; }
}