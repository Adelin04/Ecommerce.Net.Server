using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

public interface ISizeStockRepository
{
    public Task<SizeStock> RegisterNewSizeStockAsync(SizeStock sizeStock);
    public Task<List<SizeStock>> RegisterListOfNewSizeStockAsync(List<SizeStock> listSizeStock);
    public Task<SizeStock> GetSizeStockByIdProductAsync(long idProduct,long sizeId);
    public Task<List<SizeStock>> GetAllSizeStockRegisteredAsync();
    public Task<SizeStock> UpdateStockExistingSizeByIdProductAsync(long id,long newStock,long sizeId);

};