using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class SizeRepository : ISizeRepository
{
    private readonly EcommerceContext _context;

    public SizeRepository(EcommerceContext context)
    {
        this._context = context;
    }

    public async Task<Size> CreateNewSizeAsync(Size newSize)
    {
        var createdNewSize = await this._context.Sizes.AddAsync(newSize);

        if (createdNewSize.State == EntityState.Added)
        {
            await this._context.SaveChangesAsync(true);
            return newSize;
        }
        return null;
    }

    public async Task<List<Size>> GetAllSizeAsync()
    {
        var allSizes = await this._context.Sizes.ToListAsync();

        return allSizes;
    }

    public Task<Size> GetAllSizeByIdAsync(long id)
    {
        var sizeById = this._context.Sizes.FirstOrDefaultAsync(size => size.Id == id);

        return sizeById;
    }

    public Task<Size> GetAllSizeByNameAsync(string name)
    {
        var sizeByName = this._context.Sizes.FirstOrDefaultAsync(size => size.Name == name);

        return sizeByName;
    }
}