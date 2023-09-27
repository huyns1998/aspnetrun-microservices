using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.BL
{
    public interface IBasketService
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task<ShoppingCart> AddBasket(ShoppingCart basket);
        Task<ShoppingCart> DeleteBasket(int id);
        Task<ShoppingCart> DeleteAllShoppingCartItem(int id);
    }
}
