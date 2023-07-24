using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetInvoiceByIdAsync();
    }
}