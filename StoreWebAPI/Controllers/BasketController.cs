using AutoMapper;
using Core.Enitities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Dtos;

namespace StoreWebAPI.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet("GetBasketById")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost("UpdateBasket")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto dto)
        {
            var mappedbasket = _mapper.Map<CustomerBasket>(dto);
            return Ok(await _basketRepository.UpdateBasketAsync(mappedbasket));
        }
        [HttpDelete("DeleteBasket")]
        public async Task DeleteBasket(string id)
              => await _basketRepository.DeleteBasketAsync(id);
    }

}


