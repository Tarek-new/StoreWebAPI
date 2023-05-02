using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreWebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _product;

        public ProductsController(IProductRepository product )
        {
            _product = product;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
            => Ok(await _product.GetAll());

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int? id)
        { 
            if (id == null)
                return NotFound();
            
           return Ok(await _product.GetById(id));
        }

    
    }
}
