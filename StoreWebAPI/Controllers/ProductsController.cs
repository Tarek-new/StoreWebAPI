using AutoMapper;
using Core.Enitities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Dtos;

namespace StoreWebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository product,IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var Spec=new ProductWithBrandAndTypeSpecification(productSpecParams);
            var products = await _product.GetListBySpecificationsAsync(Spec);


            var mappedProducts = _mapper.Map<IList<ProductDto>>(products);
            return Ok(mappedProducts);
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            if (id == null)
                return NotFound();
            var Spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _product.GetBySpecificationsAsync(Spec);

            if (product == null)
                return NotFound();
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return Ok(mappedProduct);
        }

        //[HttpGet("GetProductwithBrandAndType")]
        //public IActionResult FindProduct([FromQuery] Expression<Func<Product, bool>> cretiria)
        //=> Ok( _product.Find(cretiria));

    }
}
