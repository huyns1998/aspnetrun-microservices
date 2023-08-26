using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        { 
            return await _productRepository.GetById(id);  
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return await _productRepository.GetByConditions(x => x.Category.Equals(category));
        }

        public async Task<Product> Add(Product product)
        {
            return await _productRepository.Add(product);
        }

        public async Task<Product> Update(Product product)
        {
            return await _productRepository.Update(product);
        }

        public async Task<Product> Delete(int id)
        {
            return await _productRepository.Delete(id);   
        }
    }
}
