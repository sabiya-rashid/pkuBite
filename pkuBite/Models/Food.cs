using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pkuBite.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

        public SubCategory? SubCategory { get; set; }

    }
}
