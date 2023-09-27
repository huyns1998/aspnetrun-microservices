using Basket.API.Dao;
using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IShoppingCartRepository: ICrudDao<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCartWithItemsById(int id);
        Task<ShoppingCart> GetShoppingCartWithItemsByUserName(string userName);
    }
}
