using Core.Entities;

namespace Core.Specification
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParams productSpecificationParams)
         :base (p => (!productSpecificationParams.BrandId.HasValue || p.ProductBrandId == productSpecificationParams.BrandId) &&
                     (!productSpecificationParams.TypeId.HasValue || p.ProductTypeId == productSpecificationParams.TypeId))
        {
            
        }
    }
}