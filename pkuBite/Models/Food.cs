namespace pkuBite.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SubcategoryId { get; set; }                                               
        public SubCategory Subcategory { get; set; }

    }
}
