using Models;
using Models.Base;
using System.Text.Json.Serialization;

namespace pkuBite.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Favourites> Favourites { get; set; } 
    }
}
