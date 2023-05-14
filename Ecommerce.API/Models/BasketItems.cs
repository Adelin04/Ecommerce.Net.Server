using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models;

public class BasketItems
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; }

    public long ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public long BasketId { get; set; }
    public virtual Basket Basket { get; set; } = null!;
}