using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

public interface ISizeStockRepository
{
    public Task<SizeStock> RegisterNewSizeStockAsync(SizeStock sizeStock);
    public Task<List<SizeStock>> RegisterListOfNewSizeStockAsync(List<SizeStock> listSizeStock);
    public Task<List<SizeStock>> GetAllSizeStockRegisteredAsync();

};