using AutoMapper;
using Core.Enitities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Dtos;
using StoreWebAPI.Helpers;
using StoreWebAPI.ResponseStatusModules;

namespace StoreWebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(productSpecParams);
            var countSpec = new ProductsWithFiltersForCountSpec(productSpecParams);
            var TotalCount = await _product.CountAsync(countSpec);
            var products = await _product.GetListBySpecificationsAsync(Spec);

            var mappedProducts = _mapper.Map<IList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(productSpecParams.PageIndex, productSpecParams.PageSize, TotalCount, mappedProducts));
        }

        [HttpGet("GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            if (id == null)
                return NotFound(new ApiResponse(404));
            var Spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _product.GetBySpecificationsAsync(Spec);

            if (product == null)
                return NotFound(new ApiResponse(404));
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return Ok(mappedProduct);
        }

        [HttpGet("GetProductwithBrandAndType")]
        public async Task<ActionResult<Product>> FindProduct(int id, [FromQuery] string[] includes)
        {

            return Ok(await _product.FindAsync(p => p.Id == id, includes));
        }




    }
}
