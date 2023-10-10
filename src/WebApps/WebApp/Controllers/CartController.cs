using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;

        public CartController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasket("swn");
            return View(basket);   
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogModel product)
        {
            var basket = await _basketService.GetBasket("swn");
            basket.Items.Add(new BasketItemModel()
            {
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await _basketService.UpdateBasket(basket);
            return RedirectToAction("Index");   
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string productName)
        {
            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);

            BasketItemModel item = basket.Items.FirstOrDefault(x => x.ProductName == productName);

            if(item != null) 
            {
                basket.Items.Remove(item);
                await _basketService.UpdateBasket(basket);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            BasketCheckoutModel basketCheckout = new BasketCheckoutModel();
            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);
            basketCheckout.Cart = basket;

            return View(basketCheckout);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(BasketCheckoutModel checkoutModel)
        {
            var userName = "swn";
            checkoutModel.Cart = await _basketService.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return View(checkoutModel); 
            }

            checkoutModel.UserName = userName;
            checkoutModel.TotalPrice = checkoutModel.Cart.TotalPrice;

            await _basketService.CheckoutBasket(checkoutModel);

            //return RedirectToPage("Confirmation", "OrderSubmitted");

            return RedirectToAction("Index");
        }
    }
}
