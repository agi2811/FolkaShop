using FolkaShop.WebApi.Data.Repository.Interface;
using FolkaShop.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly FolkaShopContext _context;

        public ProductRepository(FolkaShopContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        public List<dynamic> GetProduct(int categoryId)
        {
            var param = new Dictionary<string, object>();
            var result = _context.CollectionFromSql(@"exec [dbo].[SP_GetProduct] " + categoryId, param).ToList();
            return result;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int sku)
        {
            var entity = await _context.Product.FindAsync(sku);
            if (entity == null) return entity;

            _context.Product.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
