using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Data;


namespace Ecommerce.API.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly EcommerceContext _context;

    public InvoiceRepository(EcommerceContext context)
    {
        this._context = context;
    }
    public async Task<Invoice> GetInvoiceByIdAsync()
    {
        throw new NotImplementedException();
    }
}