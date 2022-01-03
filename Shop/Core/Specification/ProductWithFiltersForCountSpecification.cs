using Core.Entities;

namespace Core.Specification
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParams productSpecificationParams)
         : base (p => (productSpecificationParams.BrandId == 0 || !productSpecificationParams.BrandId.HasValue || p.ProductBrandId == productSpecificationParams.BrandId) &&
                     (productSpecificationParams.TypeId == 0 ||!productSpecificationParams.TypeId.HasValue || p.ProductTypeId == productSpecificationParams.TypeId) &&
                     (string.IsNullOrEmpty(productSpecificationParams.Search) || p.Name.ToLower().Contains(productSpecificationParams.Search.ToLower())))
        {
            
        }
    }
}