using Ecommerce.API.Models;
using Ecommerce.API.Repositories;
using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;

public class SizeStockRepository : ISizeStockRepository
{

    private readonly EcommerceContext _context;

    public SizeStockRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<SizeStock> RegisterNewSizeStockAsync(SizeStock newSizeStock)
    {
        var sizeStockRegistered = await this._context.SizeStocks.AddAsync(newSizeStock);
        if (sizeStockRegistered.State == EntityState.Added)
        {
            await this._context.SaveChangesAsync();
            return newSizeStock;
        }
        return null;
    }



    public async Task<List<SizeStock>> RegisterListOfNewSizeStockAsync(List<SizeStock> listSizeStock)
    {
        foreach (var size in listSizeStock)
        {
            var sizeStockRegistered = await this._context.SizeStocks.AddAsync(size);
            if (sizeStockRegistered.State == EntityState.Added)
            {
                await this._context.SaveChangesAsync(true);
            }
        }
        return listSizeStock;
    }

    public async Task<List<SizeStock>> GetAllSizeStockRegisteredAsync()
    {
        var listOfSizeStock = await this._context.SizeStocks.ToListAsync();

        return listOfSizeStock;
    }
}