using Core.Enitities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Dtos;

namespace StoreWebAPI.Controllers
{

    public class BrandsController : BaseController
    {
        private readonly IProductBrandsRepository _brandsRepository;

        public BrandsController(IProductBrandsRepository brandsRepository)
        {
            _brandsRepository= brandsRepository;
        }

        /// <summary>
        /// Get All Brands
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrandsAsync()
            => Ok(await _brandsRepository.GetAll());

        /// <summary>
        /// Get Brand by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBrand{id}")]
        public async Task<IActionResult> GetBrandAsync(int? id)
        {
            if (id == null)
                return BadRequest();
            var brand= await _brandsRepository.GetById(id);
            if (brand == null)
                return NotFound();
            return Ok(await _brandsRepository.GetById(id));
        }

        [HttpPost("AddBrand")]
        public async Task<IActionResult> CreateAsync( BrandDto dto)
        {
            var existingBrand=await _brandsRepository.GetById(dto.Id);
            if (existingBrand != null)
            {
                return BadRequest("Brand with the specified Id already exists.");
            }

            var brand = new ProductBrand()
            {
                Id=dto.Id,
                Name=dto.Name,
            };

            await _brandsRepository.Add(brand);
            return Ok(dto);
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateAsync(BrandDto dto)
        {
            var existingBrand = await _brandsRepository.GetById(dto.Id);
            if (existingBrand == null)
            {
                return BadRequest($"No brand was found with ID :{dto.Id}");
            }

            existingBrand.Name = dto.Name;
            existingBrand.Id= dto.Id;

            await _brandsRepository.Update(existingBrand);
            return Ok(dto);
        }
    }
    
}
