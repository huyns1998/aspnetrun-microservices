using System.ComponentModel.DataAnnotations;

namespace Basket.API.Dtos
{
    public class ShoppingCartItemReadDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }
}
