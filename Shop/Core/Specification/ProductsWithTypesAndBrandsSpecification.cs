using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParams productSpecificationParams) :
               base (p => (productSpecificationParams.BrandId == 0 || !productSpecificationParams.BrandId.HasValue || p.ProductBrandId == productSpecificationParams.BrandId) &&
                          (productSpecificationParams.TypeId == 0 || !productSpecificationParams.TypeId.HasValue || p.ProductTypeId == productSpecificationParams.TypeId) &&
                          (string.IsNullOrEmpty(productSpecificationParams.Search) || p.Name.ToLower().Contains(productSpecificationParams.Search.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplyPaging(productSpecificationParams.PageSize * (productSpecificationParams.PageIndex - 1), productSpecificationParams.PageSize);
         
            switch(productSpecificationParams.Sort)
            {
                case "price-asc": 
                    AddOrderBy(p => p.Price); 
                    break;
                case "price-desc":
                    AddOrderByDescending(p => p.Price); 
                    break;
                default :
                    AddOrderBy(p => p.Name); 
                    break;
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}