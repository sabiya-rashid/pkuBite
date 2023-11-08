using Models;
using Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pkuBite.Models
{
    public class FoodItem : BaseModel
    {
        //[Key]
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Description { get; set; }
        public double Price { get; set; }

        //[ForeignKey("SubCategoryId")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Favourites> Favourites { get; set; }
    }
}
