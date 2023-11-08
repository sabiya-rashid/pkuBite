using DbContext.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using pkuBite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContext
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<FoodItem> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FoodItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FavouriteConfiguration());

            modelBuilder.Entity<Category>().HasData(
    new Category { Id = 1, Name = "Plant foods" },
    new Category { Id = 2, Name = "Savoury foods" },
    new Category { Id = 3, Name = "Diary foods" },
    new Category { Id = 4, Name = "Drinks" },
    new Category { Id = 5, Name = "Snack foods" },
    new Category { Id = 6, Name = "Baking foods" },
    new Category { Id = 7, Name = "Sweets, Spreads" }
);
            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { Id = 1, Name = "Nuts and seeds", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Dried Fruits", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "Meat, Fish, Eggs", CategoryId = 2 },
                new SubCategory { Id = 4, Name = "Vegeterian", CategoryId = 2 },
                new SubCategory { Id = 5, Name = "Regular cheese", CategoryId = 3 },
                new SubCategory { Id = 6, Name = "Milk products", CategoryId = 3 },
                new SubCategory { Id = 7, Name = "Tea and coffee", CategoryId = 4 },
                new SubCategory { Id = 8, Name = "Fizzy drinks and Squash juice", CategoryId = 4 },
                new SubCategory { Id = 9, Name = "Sweet snacks", CategoryId = 5 },
                new SubCategory { Id = 10, Name = "Salty snacks", CategoryId = 5 },
                new SubCategory { Id = 11, Name = "Cakes", CategoryId = 6 },
                new SubCategory { Id = 12, Name = "Biscuits", CategoryId = 6 },
                new SubCategory { Id = 13, Name = "Sugars and syrups", CategoryId = 7 },
                new SubCategory { Id = 14, Name = "Confectionary", CategoryId = 7 }
            );
            modelBuilder.Entity<FoodItem>().HasData(
    new FoodItem { Id = 1, Name = "Almonds", Price = 5.99, Description = "Fresh almonds", SubCategoryId = 1, ImageUrl="imageurl"},
    new FoodItem { Id = 2, Name = "Cashews", Price = 6.49, Description = "Roasted cashews", SubCategoryId = 1, ImageUrl = "imageurl" },
    new FoodItem { Id = 3, Name = "Raisins", Price = 3.99, Description = "Sun-dried raisins", SubCategoryId = 2, ImageUrl = "imageurl" },
    new FoodItem { Id = 4, Name = "Apricots", Price = 7.99, Description = "Dried apricots", SubCategoryId = 2 , ImageUrl = "imageurl" },
    new FoodItem { Id = 5, Name = "Chicken", Price = 8.99, Description = "Fresh chicken", SubCategoryId = 3 , ImageUrl = "imageurl" },
    new FoodItem { Id = 6, Name = "Salmon", Price = 12.99, Description = "Wild-caught salmon", SubCategoryId = 3 , ImageUrl = "imageurl" },
    new FoodItem { Id = 7, Name = "Tofu", Price = 4.49, Description = "Organic tofu", SubCategoryId = 4 , ImageUrl = "imageurl" },
    new FoodItem { Id = 8, Name = "Eggplant", Price = 2.99, Description = "Fresh eggplant", SubCategoryId = 4 , ImageUrl = "imageurl" },
    new FoodItem { Id = 9, Name = "Cheddar Cheese", Price = 4.99, Description = "Sharp cheddar cheese", SubCategoryId = 5 , ImageUrl = "imageurl" },
    new FoodItem { Id = 10, Name = "Mozzarella Cheese", Price = 5.49, Description = "Fresh mozzarella cheese", SubCategoryId = 5 , ImageUrl = "imageurl" },
    new FoodItem { Id = 11, Name = "Milk", Price = 2.29, Description = "Whole milk", SubCategoryId = 6 , ImageUrl = "imageurl" },
    new FoodItem { Id = 12, Name = "Yogurt", Price = 3.49, Description = "Greek yogurt", SubCategoryId = 6 , ImageUrl = "imageurl" },
    new FoodItem { Id = 13, Name = "Black Tea", Price = 3.99, Description = "Premium black tea", SubCategoryId = 7 , ImageUrl = "imageurl" },
    new FoodItem { Id = 14, Name = "Coffee", Price = 6.99, Description = "Arabica coffee", SubCategoryId = 7 , ImageUrl = "imageurl" },
    new FoodItem { Id = 15, Name = "Soda", Price = 1.99, Description = "Carbonated soda", SubCategoryId = 8 , ImageUrl = "imageurl" },
    new FoodItem { Id = 16, Name = "Fruit Juice", Price = 4.49, Description = "Assorted fruit juices", SubCategoryId = 8 , ImageUrl = "imageurl" },
    new FoodItem { Id = 17, Name = "Chocolate", Price = 2.99, Description = "Dark chocolate bar", SubCategoryId = 9 , ImageUrl = "imageurl" },
    new FoodItem { Id = 18, Name = "Candy", Price = 1.49, Description = "Assorted candies", SubCategoryId = 9 , ImageUrl = "imageurl" },
    new FoodItem { Id = 19, Name = "Potato Chips", Price = 2.79, Description = "Classic potato chips", SubCategoryId = 10 , ImageUrl = "imageurl" },
    new FoodItem { Id = 20, Name = "Pretzels", Price = 2.29, Description = "Crunchy pretzels", SubCategoryId = 10 , ImageUrl = "imageurl" },
    new FoodItem { Id = 21, Name = "Chocolate Cake", Price = 8.99, Description = "Decadent chocolate cake", SubCategoryId = 11 , ImageUrl = "imageurl" },
    new FoodItem { Id = 22, Name = "Carrot Cake", Price = 7.49, Description = "Homemade carrot cake", SubCategoryId = 11 , ImageUrl = "imageurl" },
    new FoodItem { Id = 23, Name = "Chocolate Chip Cookies", Price = 3.99, Description = "Freshly baked cookies", SubCategoryId = 12 , ImageUrl = "imageurl" },
    new FoodItem { Id = 24, Name = "Oatmeal Cookies", Price = 3.49, Description = "Healthy oatmeal cookies", SubCategoryId = 12 , ImageUrl = "imageurl" },
    new FoodItem { Id = 25, Name = "Sugar", Price = 2.49, Description = "Granulated sugar", SubCategoryId = 13 , ImageUrl = "imageurl" },
    new FoodItem { Id = 26, Name = "Maple Syrup", Price = 5.99, Description = "Pure maple syrup", SubCategoryId = 13 , ImageUrl = "imageurl" },
    new FoodItem { Id = 27, Name = "Gummy Bears", Price = 1.99, Description = "Assorted gummy bears", SubCategoryId = 14, ImageUrl = "imageurl" },
    new FoodItem { Id = 28, Name = "Licorice", Price = 2.29, Description = "Black licorice twists", SubCategoryId = 14 , ImageUrl = "imageurl" }
         );

        }

    }
}
