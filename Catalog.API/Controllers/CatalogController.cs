using AutoMapper;
using Catalog.API.BL;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        //private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductService productServive/*, ILogger<CatalogController> logger*/, IMapper mapper)
        {
            _productService = productServive;
            _mapper = mapper;   
            //_logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> Get()
        {
            Console.WriteLine("--> Get all products");

            var products = await _productService.GetAll();
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Console.WriteLine($"--> Get product by id {id}");

            var product = await _productService.GetById(id);

            if (product == null)
            {
                Console.WriteLine($"--> Notfound product with id {id}");
                return BadRequest();
            }

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpGet("{category}/category")]
        public async Task<ActionResult<Product>> Category(string category)
        {
            Console.WriteLine($"--> Get product by category {category}");

            var product = await _productService.GetProductByCategory(category);

            if (product == null)
            {
                Console.WriteLine($"--> Notfound product with category {category}");
                return BadRequest();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductCreateDto productCreateDto)
        {
            Console.WriteLine($"--> Create product {productCreateDto.Name}");

            Product product = _mapper.Map<Product>(productCreateDto);   

            var result = await _productService.Add(product);
            if (result != null)
            {
                Console.WriteLine($"--> Create product {product.Name} successully");
                return Created($"Create Product Id {result.Id}", result);
            }
            else
            {
                Console.WriteLine($"--> Create product {result.Name} fail");
                return BadRequest();    
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put([FromBody] ProductUpdateDto productUpdateDto)
        {
            Console.WriteLine($"--> Update product {productUpdateDto.Name}");

            var product = await _productService.GetById(productUpdateDto.Id);
            if (product == null)
            {
                Console.WriteLine($"Notfound product id {product.Id} to update");
                return BadRequest();
            }

            product.Name = productUpdateDto.Name;   
            product.Summary = productUpdateDto.Summary;
            product.Category = productUpdateDto.Category;
            product.Description = productUpdateDto.Description;
            product.Price = productUpdateDto.Price;
            product.ImageFile = productUpdateDto.ImageFile;

            var result = await _productService.Update(product);

            if(result == null) 
            {
                return BadRequest(result);    
            }

            return Ok(result);    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return BadRequest(product);
            }

            var result = await _productService.Delete(id);
            return Ok(result);
        }
    }
}
