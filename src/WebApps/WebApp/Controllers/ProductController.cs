using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string category = null)
        {
            if(string.IsNullOrEmpty(category)) 
            {
                IEnumerable<CatalogModel> productList = await _catalogService.GetCatalog();
                return View(productList);
            }
            else
            {
                IEnumerable<CatalogModel> productList = await _catalogService.GetCatalogByCategory(category);
                return View(productList);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            CatalogModel product = await _catalogService.GetCatalog(id);
            return View(product);
        }
    }
}
