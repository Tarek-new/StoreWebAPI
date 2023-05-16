using Core.Enitities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpec : BaseSpecifcation<Product>
    {
        public ProductsWithFiltersForCountSpec(ProductSpecParams productSpecParams)
           : base(product =>
                 (String.IsNullOrEmpty(productSpecParams.Search) || product.Name.ToLower().Contains(productSpecParams.Search)) &&
                 (!productSpecParams.BrandId.HasValue || product.ProductBrandId == productSpecParams.BrandId) &&
                 (!productSpecParams.TypeId.HasValue || product.ProductTypeId == productSpecParams.TypeId)
                 )
        {
        }
    }
}
