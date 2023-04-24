using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models;

public class Basket
{
    public long Id { get; set; }
    public long BuyerId { get; set; }
    public List<BasketItems> Items { get; set; } = new();
}