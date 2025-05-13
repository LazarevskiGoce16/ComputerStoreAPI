using ComputerStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStoreAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
