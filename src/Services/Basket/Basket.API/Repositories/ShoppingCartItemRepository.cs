using Basket.API.Dao;
using Basket.API.Data;
using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public class ShoppingCartItemRepository: ACrudDao<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(BasketDbContext context) : base(context)
        {
        }
    }
}
