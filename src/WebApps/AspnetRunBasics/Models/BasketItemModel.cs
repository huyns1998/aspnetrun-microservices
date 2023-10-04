namespace AspnetRunBasics.Models
{
    public class BasketItemModel
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
