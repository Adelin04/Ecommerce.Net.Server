using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface ISizeRepository
{
    public Task<Size> CreateNewSizeAsync(Size newSize);
    public Task<List<Size>> GetAllSizeAsync();
    public Task<Size> GetSizeByIdAsync(long id);
    public Task<Size> GetSizeByNameAsync(string name);
}