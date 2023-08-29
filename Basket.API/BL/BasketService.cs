using Basket.API.Entities;
using Basket.API.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.BL
{
    public class BasketService : IBasketService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        public BasketService(IShoppingCartRepository shoppingCartRepository,
           IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartRepository= shoppingCartRepository;    
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task<ShoppingCart> AddBasket(ShoppingCart basket)
        {
            return await _shoppingCartRepository.Add(basket);
        }

        public async Task<ShoppingCart> DeleteAllShoppingCartItem(int id)
        {
            var basket = await _shoppingCartRepository.GetShoppingCartWithItemsById(id);
            basket.Items.Clear();   
            return await _shoppingCartRepository.Update(basket);
        }

        public async Task<ShoppingCart> DeleteBasket(int id)
        {
            return await _shoppingCartRepository.Delete(id);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            return await _shoppingCartRepository.GetShoppingCartWithItemsByUserName(userName);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            return await _shoppingCartRepository.Update(basket);
        }
    }
}
