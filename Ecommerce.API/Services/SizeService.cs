using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Ecommerce.API.Contracts;

namespace Ecommerce.API.Services;

public class SizeService
{
    private readonly ISizeRepository _sizeRepository;

    public SizeService(ISizeRepository sizeRepository)
    {
        this._sizeRepository = sizeRepository;
    }

    public async Task<List<Size>> GetAllSize_ServiceAsync()
    {
        var allSizes = await this._sizeRepository.GetAllSizeAsync();
        return allSizes;
    }

    public async Task<Size> CreateNewSize(SizeDataRegister sizeDataRegister)
    {
        Size newSize = null;


        if (sizeDataRegister is null) return null;

        newSize = new Size();
        newSize.Name = sizeDataRegister.SizeName;

        var createdNewSize = await this._sizeRepository.CreateNewSizeAsync(newSize);

        return createdNewSize;
    }
}