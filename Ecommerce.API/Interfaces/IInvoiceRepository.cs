using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface IInvoiceRepository
{
    public Task<Invoice> CreateInvoiceAsync(Invoice newInvoice);
    public Task<Invoice> GetInvoiceByIdAsync();
}
