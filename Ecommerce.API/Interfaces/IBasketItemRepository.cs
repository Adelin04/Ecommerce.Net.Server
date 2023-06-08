using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces
{
    public interface IBasketItemRepository
    {
        public Task<BasketItems> AddNewBasketItem(BasketItems newBasketItem);
        public Task<List<BasketItems>> GetAllBasketItems();

        public Task<List<BasketItems>> GetBasketItemsByUserId(long id);
        public Task<BasketItems> DecrementBasketItemQuantityById(long id);
        public Task<BasketItems> GetBasketItemsById(long id);
        public Task<BasketItems> DeleteBasketItemsByProductId(long id);
        public Task<BasketItems> DeleteBasketItemsById(long id);
    }
}