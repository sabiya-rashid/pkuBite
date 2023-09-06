using Microsoft.EntityFrameworkCore;
using pkuBite.Models.Models;

namespace pkuBite.Data.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }   
    public DbSet<Food> Foods { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Clothing" }
        );

        // Seed SubCategories
        modelBuilder.Entity<SubCategory>().HasData(
            new SubCategory { Id = 1, Name = "Phones", Category_id = 1 },
            new SubCategory { Id = 2, Name = "Laptops", Category_id = 1 },
            new SubCategory { Id = 3, Name = "T-Shirts", Category_id = 2 },
            new SubCategory { Id = 4, Name = "Jeans", Category_id = 2 }
        );

        // Seed Foods
        modelBuilder.Entity<Food>().HasData(
            new Food { Id = 1, Name = "iPhone", Description = "Latest smartphone", SubCategoryId = 1 },
            new Food { Id = 2, Name = "Dell XPS", Description = "High-performance laptop", SubCategoryId = 2 },
            new Food { Id = 3, Name = "Basic T-Shirt", Description = "Casual clothing", SubCategoryId = 3 },
            new Food { Id = 4, Name = "Slim Fit Jeans", Description = "Stylish denim", SubCategoryId = 4 }
        );
    }
}
