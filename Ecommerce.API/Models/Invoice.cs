using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class Invoice
    {
        public long Id { get; set; }

        public virtual User User { get; set; }

        public long AddressCustomerId { get; set; }
        // public virtual AddressCustomer AddressCustomer { get; set; }
    }
}