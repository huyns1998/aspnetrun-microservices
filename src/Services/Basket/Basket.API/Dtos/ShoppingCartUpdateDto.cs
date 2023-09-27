using System.Collections.Generic;

namespace Basket.API.Dtos
{
    public class ShoppingCartUpdateDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemUpdateDto> Items { get; set; }
    }
}
