using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.API.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly EcommerceContext _context;

    public InvoiceRepository(EcommerceContext context)
    {
        this._context = context;
    }


   public async Task<Invoice?> CreateInvoiceAsync(Invoice newInvoice)
    {
        var newInvoiceCreated = await this._context.AddAsync(newInvoice);

        if (newInvoiceCreated.State is EntityState.Added)
        {
            await this._context.SaveChangesAsync();
            return newInvoice;
        }
        return null;
    }

    Task<Invoice?> IInvoiceRepository.GetInvoiceByIdAsync()
    {
        throw new NotImplementedException();
    }
}