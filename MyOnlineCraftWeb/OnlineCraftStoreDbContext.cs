using Microsoft.EntityFrameworkCore;
using MyOnlineCraftWeb.Models;

namespace MyOnlineCraftWeb
{
    public class OnlineCraftStoreDbContext:DbContext
    {
        public OnlineCraftStoreDbContext(DbContextOptions<OnlineCraftStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
