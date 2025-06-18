namespace ConsumingWebAPIMVCApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Manufacturing_Cost { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public int Selling_Price { get; set; }
        public bool ISActive { get; set; }
    }
}
