using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> GetByProductName(string productName);
        Task<IEnumerable<Product>> GetProductByCategory(string category);
        public Task<Product> Add(Product product);
        public Task<Product> Update(Product product);
        public Task<Product> Delete(int id);    
    }
}
