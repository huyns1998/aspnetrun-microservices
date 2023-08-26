using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalog.API.Data
{
    public class CatalogDbContext: DbContext 
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> opt) : base(opt)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
