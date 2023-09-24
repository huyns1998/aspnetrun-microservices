using AutoMapper;
using Basket.API.BL;
using Basket.API.Dtos;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly DiscountGrpcService _discountGrpcService;
        //private readonly ILogger<CatalogController> _logger;

        public BasketController(IBasketService basketService/*, ILogger<CatalogController> logger*/, IMapper mapper, DiscountGrpcService discountGrpcService)
        {
            _basketService = basketService;
            _mapper = mapper;
            _discountGrpcService = discountGrpcService;
            //_logger = logger;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> Get(string userName)
        {
            Console.WriteLine("--> Get basket by userName");
            var basket = await _basketService.GetBasket(userName);
            if(basket == null) 
            {
                ShoppingCartCreateDto cartCreateDto = new ShoppingCartCreateDto()
                {
                    UserName = userName
                };
                var cart = _mapper.Map<ShoppingCart>(cartCreateDto);    
                var result = await _basketService.AddBasket(cart);
               return Created($"Create basket Id {result.Id}", _mapper.Map<ShoppingCartReadDto>(result));
            }

            return Ok(_mapper.Map<ShoppingCartReadDto>(basket));
        } 

        [HttpPut]
        public async Task<ActionResult<ShoppingCartUpdateDto>> Put([FromBody] ShoppingCartUpdateDto cartUpdateDto)
        {
            Console.WriteLine($"--> Update ShoppingCart {cartUpdateDto.UserName}");

            var cart = await _basketService.GetBasket(cartUpdateDto.UserName);
            if (cart == null)
            {
                Console.WriteLine($"Notfound shoppingCard userName {cartUpdateDto.UserName} to update");
                return BadRequest();
            }

            await _basketService.DeleteAllShoppingCartItem(cart.Id);

            _mapper.Map(cartUpdateDto, cart);

            foreach(var item in cart.Items)
            {
                try
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                    item.Price -= coupon.Amount;
                }
                catch
                {
                    Console.WriteLine($"--> Not found discount for {item.ProductName}");
                }
            }

            var result = await _basketService.UpdateBasket(cart);

            return Ok(_mapper.Map<ShoppingCartReadDto>(result));
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            var basket = await _basketService.GetBasket(userName);

            if (basket == null)
            {
                return BadRequest();
            }

            var result = await _basketService.DeleteBasket(basket.Id);
            return Ok(result);
        }
    }
}
