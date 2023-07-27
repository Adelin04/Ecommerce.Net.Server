using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class Order
    {
        public long Id { get; set; }

        public virtual User User { get; set; }

        public long? AddressCustomerId { get; set; }
    }
}