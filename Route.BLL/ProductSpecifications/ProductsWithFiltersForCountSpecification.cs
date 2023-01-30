using Route.BLL.Specifications;
using Route.DAL.Entities;

namespace Route.BLL.ProductSpecifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {

        public ProductsWithFiltersForCountSpecification(ProductsSpecParams productsSpec)
            : base(
                 p =>
                 (string.IsNullOrEmpty(productsSpec.Search) || p.Name.ToLower().Contains(productsSpec.Search)) &&
                 (!productsSpec.BrandId.HasValue || p.ProductBrandId == productsSpec.BrandId.Value) &&
                 (!productsSpec.TypeId.HasValue || p.ProductTypeId == productsSpec.TypeId.Value)
                 )
        {

        }

    }
}
