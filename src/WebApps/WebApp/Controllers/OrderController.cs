using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService= orderService;    
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersByUserName("swn");

            return View(orders);
        }
    }
}
