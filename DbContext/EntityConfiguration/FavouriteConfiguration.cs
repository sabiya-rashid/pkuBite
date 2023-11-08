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
    public class FavouriteConfiguration : IEntityTypeConfiguration<Favourites>
    { 
        public void Configure(EntityTypeBuilder<Favourites> builder)
        {
           builder.HasKey(x => x.Id); 
           builder.HasOne(x => x.User)
                .WithMany(x=> x.Favourites)
                .HasForeignKey(x=>x.UserId);
            builder.HasOne(x=>x.Food)
                .WithMany(x=> x.Favourites)
                .HasForeignKey(x=>x.FoodId);
        }
    }
}
