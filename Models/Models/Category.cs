using System.ComponentModel.DataAnnotations;

namespace pkuBite.Models.Models;

public class Category : Base
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

}
