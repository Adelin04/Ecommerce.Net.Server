using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class Invoice
    {
        public long id { get; set; }
        public AddressCustomer AddressCustomer { get; set; }
        public User User { get; set; }
    }
}