using Core.Enitities;

namespace Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifcation<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams productSpecParams)
            : base(product =>
                  (String.IsNullOrEmpty(productSpecParams.Search) || product.Name.ToLower().Contains(productSpecParams.Search)) &&
                  (!productSpecParams.BrandId.HasValue || product.ProductBrandId == productSpecParams.BrandId) &&
                  (!productSpecParams.TypeId.HasValue || product.ProductTypeId == productSpecParams.TypeId)
                  )
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            Paging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }

        public ProductWithBrandAndTypeSpecification(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
    }
}
