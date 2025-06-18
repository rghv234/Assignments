namespace ProductServices.Models
{
    public class Product
    {
        public int Id { get; set; } // Unique identifier
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
