using Catalog.API.Dao;
using Catalog.API.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository: ICrudDao<Product>
    {
        
    }
}
