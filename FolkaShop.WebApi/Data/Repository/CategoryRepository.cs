using FolkaShop.WebApi.Data.Repository.Interface;
using FolkaShop.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly FolkaShopContext _context;

        public CategoryRepository(FolkaShopContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategory()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var entity = await _context.Category.FindAsync(id);
            if (entity == null) return entity;

            _context.Category.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
