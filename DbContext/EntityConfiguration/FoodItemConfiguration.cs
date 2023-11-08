using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pkuBite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContext.EntityConfiguration
{
    public class FoodItemConfiguration : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x=>x.Description).HasMaxLength(500);
            builder.Property(x=> x.Price).HasMaxLength(100);

            builder.HasOne(x=>x.SubCategory)
                .WithMany(x=>x.FoodItems)
                .HasForeignKey(x=> x.SubCategoryId);
            

        }
    }
}
