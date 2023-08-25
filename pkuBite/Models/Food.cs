using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pkuBite.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public double Price { get; set; }

        [ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }                                               

    }
}
