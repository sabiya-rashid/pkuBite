using Microsoft.EntityFrameworkCore;
using pkuBite.Models;

namespace pkuBite.Data
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SubCategory>()
        //     .HasOne(s => s.Category)
        //     .WithMany(c => c.SubCategories)
        //     .HasForeignKey(s => s.CategoryId);

        //    modelBuilder.Entity<Food>()
        //        .HasOne(f => f.Subcategory)
        //        .WithMany(s => s.FoodItems)
        //        .HasForeignKey(f => f.SubcategoryId);
        //}
    }
}
