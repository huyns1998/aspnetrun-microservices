using AutoMapper;
using Discount.API.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Discount.API.Dtos;
using Discount.API.Entities;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public DiscountController(ICouponService couponService, IMapper mapper)
        {
            _couponService = couponService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponReadDto>>> Get()
        {
            Console.WriteLine("--> Get all discount");

            var coupons = await _couponService.GetAll();
            return Ok(_mapper.Map<IEnumerable<CouponReadDto>>(coupons));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CouponReadDto>>> Get(int id)
        {
            Console.WriteLine("--> Get discount by id");

            var coupon = await _couponService.GetById(id);
            return Ok(_mapper.Map<CouponReadDto>(coupon));
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> Post([FromBody] CouponCreateDto couponCreateDto)
        {
            Console.WriteLine($"--> Create discount for {couponCreateDto.ProductName}");

            Coupon coupon = _mapper.Map<Coupon>(couponCreateDto);

            try
            {
                var result = await _couponService.Add(coupon);
                if (result != null)
                {
                    Console.WriteLine($"--> Create discount for {result.ProductName} successully");
                    return Created($"Create Product Id {result.Id}", result);
                }
                else
                {
                    Console.WriteLine($"--> Create discount for {coupon.ProductName} fail");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(couponCreateDto.ProductName);
            } 
        }

        [HttpPut]
        public async Task<ActionResult<Coupon>> Put([FromBody] CouponUpdateDto couponUpdateDto)
        {
            Console.WriteLine($"--> Update discount for  {couponUpdateDto.ProductName}");

            var coupon = await _couponService.GetById(couponUpdateDto.Id);
            if (coupon == null)
            {
                Console.WriteLine($"Notfound discount with id {couponUpdateDto.Id} to update");
                return BadRequest();
            }

            _mapper.Map(couponUpdateDto, coupon);

            var result = await _couponService.Update(coupon);
            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _couponService.GetById(id);

            if (product == null)
            {
                return BadRequest(product);
            }

            var result = await _couponService.Delete(id);
            return Ok(result);
        }
    }
}
