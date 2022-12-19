using FolkaShop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Data.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProduct();
        List<dynamic> GetProduct(int categoryId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int sku);
    }
}
