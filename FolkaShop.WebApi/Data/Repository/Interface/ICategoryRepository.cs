using FolkaShop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Data.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategory();
        Task<Category> GetCategory(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int id);
    }
}
