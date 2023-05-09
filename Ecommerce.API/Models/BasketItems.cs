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
    public long BasketId { get; set; }
    public Product Product { get; set; }
    public Basket Basket { get; set; }
}