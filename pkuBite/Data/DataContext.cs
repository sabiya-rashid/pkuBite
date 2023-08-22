using Microsoft.EntityFrameworkCore;
using pkuBite.Models;

namespace pkuBite.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }   
        public DbSet<Food> Foods { get; set; }
    }
}
