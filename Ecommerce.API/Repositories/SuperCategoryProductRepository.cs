using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class SuperCategoryProductRepository : ISuperCategoryProductRepository
    {

        private readonly EcommerceContext _context;

        public SuperCategoryProductRepository(EcommerceContext context)
        {
            this._context = context;
        }

        public async Task<SuperCategoryProduct> AddNewSuperCategoryProductAsync(SuperCategoryProduct newSuperCategoryProduct)
        {
            var newSuperCategoryProductCreated = await this._context.SuperCategoryProducts.AddAsync(newSuperCategoryProduct);

            if (newSuperCategoryProductCreated.State == EntityState.Added)
            {
                await this._context.SaveChangesAsync();
                return newSuperCategoryProduct;
            }

            return null!;
        }


        public async Task<List<SuperCategoryProduct>> GetAllSuperCategoriesProductAsync() => await this._context.SuperCategoryProducts.ToListAsync();


        public async Task<SuperCategoryProduct?> GetSuperCategoryProductByIdAsync(long id) =>
                await this._context.SuperCategoryProducts.FirstOrDefaultAsync(superCategory => superCategory.Id == id);

        public async Task<SuperCategoryProduct?> GetSuperCategoryProductByNameAsync(string nameSuperCategory) => await this._context.SuperCategoryProducts.FirstOrDefaultAsync(superCategoryProduct => superCategoryProduct.Name == nameSuperCategory);

        public async Task<SuperCategoryProduct> DeleteSuperCategoryProductByNameAsync(string nameSuperCategory)
        {
            var existSuperCategoryProductByName = await this._context.SuperCategoryProducts.FirstOrDefaultAsync(categoryProduct => categoryProduct.Name == nameSuperCategory);

            if (existSuperCategoryProductByName is not null)
            {
                var removedSuperCategoryName = this._context.SuperCategoryProducts.Remove(existSuperCategoryProductByName);

                if (removedSuperCategoryName.State is EntityState.Deleted)
                {
                    await this._context.SaveChangesAsync();
                }
            }

            return existSuperCategoryProductByName;
        }

    }
}