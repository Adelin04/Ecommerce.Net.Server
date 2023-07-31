using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class AddressCustomer
    {
        public long id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }

        public long? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}