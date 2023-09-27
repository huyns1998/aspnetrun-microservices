using Basket.API.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Dtos
{
    public class ShoppingCartReadDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<ShoppingCartItemReadDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
