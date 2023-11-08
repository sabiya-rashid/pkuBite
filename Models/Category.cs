using Models.Base;
using System.Text.Json.Serialization;

namespace pkuBite.Models
{
    public class Category : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<SubCategory> SubCategories { get; set;}
    }
}
