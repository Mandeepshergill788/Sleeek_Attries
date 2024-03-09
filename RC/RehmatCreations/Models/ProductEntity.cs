namespace RehmatCreations.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Title { get; set; }
        public long Price { get; set; }
        public long Quanity { get; set; }
        public string ImagePath { get; set; }
    }
}
