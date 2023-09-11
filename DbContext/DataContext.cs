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
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<FoodItem> Foods { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
