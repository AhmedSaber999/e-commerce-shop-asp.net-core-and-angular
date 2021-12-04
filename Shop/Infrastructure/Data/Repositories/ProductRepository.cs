using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public IReadOnlyList<ProductBrand> GetProductBrands()
        {
            return context.ProductBrands.ToList();
        }

        public Product GetProduct(int id)
        {
            return context.Products.Where(p => p.Id == id)
                    .Include(p => p.ProductBrand)
                    .Include(p => p.ProductType).FirstOrDefault();
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return context.Products.Include(p => p.ProductType) 
                                    .Include(p => p.ProductBrand).ToList();
        }

        public IReadOnlyList<ProductType> GetProductTypes()
        {
            return context.ProductTypes.ToList();
        }

    }
}