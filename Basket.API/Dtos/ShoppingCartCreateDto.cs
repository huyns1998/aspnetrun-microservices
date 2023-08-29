using System.Collections.Generic;

namespace Basket.API.Dtos
{
    public class ShoppingCartCreateDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemReadDto> Items { get; set; }
    }
}
