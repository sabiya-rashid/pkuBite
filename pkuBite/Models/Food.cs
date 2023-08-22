namespace pkuBite.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SubCategory SubCategory { get; set; }
        public int Sub_Cat { get; set; }

    }
}
