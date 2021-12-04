using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        IReadOnlyList<Product> GetProducts();
        IReadOnlyList<ProductBrand> GetProductBrands();
        IReadOnlyList<ProductType> GetProductTypes();
        Product GetProduct(int id);
    }
}