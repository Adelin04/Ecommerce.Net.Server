using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces;

public interface ISizeRepository
{
    public Task<Size> CreateNewSizeAsync(Size newSize);
    public Task<List<Size>> GetAllSizeAsync();
    public Task<Size> GetAllSizeByIdAsync(long id);
    public Task<Size> GetAllSizeByNameAsync(string name);
}