namespace Ecommerce.API.Models;

public class Size
{
    public Size()
    {
        this.SizesStocks = new List<SizeStock>();
    }


    public long Id { get; set; }
    public string Name { get; set; }

    // Relationship SizeStock
    public virtual ICollection<SizeStock> SizesStocks { get; set; }
}