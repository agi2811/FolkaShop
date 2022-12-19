using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Models
{
    public class FolkaShopContext : DbContext
    {
        public FolkaShopContext(DbContextOptions<FolkaShopContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
