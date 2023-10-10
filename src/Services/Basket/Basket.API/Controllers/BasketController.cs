using AutoMapper;
using Basket.API.BL;
using Basket.API.Dtos;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public BasketController(IBasketService basketService, IMapper mapper, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _basketService = basketService;
            _mapper = mapper;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint; 
            _configuration = configuration; 
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> Get(string userName)
        {
            Console.WriteLine("--> Get basket by userName");
            var basket = await _basketService.GetBasket(userName);
            if (basket == null)
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

            foreach (var item in cart.Items)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(_configuration["HttpSettings:CatalogUrl"]);
                    HttpResponseMessage response = await httpClient.GetAsync($"api/v1/catalog/{item.ProductName}/price");

                    if (response.IsSuccessStatusCode)
                    {
                        var taskPrice = response.Content.ReadAsStringAsync();
                        var taskCoupon = _discountGrpcService.GetDiscount(item.ProductName);

                        await Task.WhenAll(taskPrice, taskCoupon);

                        decimal price = JsonSerializer.Deserialize<decimal>(taskPrice.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        Console.WriteLine($"Response content: {taskPrice.Result}");


                        item.Price = price; 
                        
                        item.Price -= taskCoupon.Result.Amount;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }     
                }
                catch
                {
                    Console.WriteLine($"--> Not found discount for {item.ProductName} or Product {item.ProductName}");
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

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckoutDto basketCheckoutDto)
        {
            ShoppingCart basket = await _basketService.GetBasket(basketCheckoutDto.UserName);
            if (basket == null) 
            {
                return BadRequest();    
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckoutDto);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);
            await _basketService.DeleteBasket(basket.Id);

            return Accepted();  
        }
    }
}
