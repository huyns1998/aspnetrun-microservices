using Basket.API.Dao;
using Basket.API.Data;
using Basket.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class ShoppingCartRepository : ACrudDao<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(BasketDbContext context) : base(context)
        {
        }

        public async Task<ShoppingCart> GetShoppingCartWithItemsById(int id)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Where(c => c.Id == id)
                .Include(x => x.Items)
                .FirstOrDefaultAsync();
            return shoppingCart;
        }

        public async Task<ShoppingCart> GetShoppingCartWithItemsByUserName(string userName)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Where(c => c.UserName.Equals(userName))
                .Include(x => x.Items)
                .FirstOrDefaultAsync();
            return shoppingCart;
        }
    }
}
