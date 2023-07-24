using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    public Task<Invoice> GetInvoiceByIdAsync()
    {
        throw new NotImplementedException();
    }
}