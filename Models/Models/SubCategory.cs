using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pkuBite.Models.Models;

public class SubCategory : Base
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [ForeignKey("Category")]
    public int Category_id { get; set; }
    
    public Category? Category { get; set; }

}
