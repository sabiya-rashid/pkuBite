namespace pkuBite.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int Cat_id { get; set; }       
    }
}
