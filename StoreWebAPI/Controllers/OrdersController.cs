using AutoMapper;
using Core.Enitities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Dtos;
using StoreWebAPI.Extensions;
using StoreWebAPI.ResponseStatusModules;

namespace StoreWebAPI.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("CreateOrder")]

        public async Task<ActionResult<Core.Enitities.OrderAggregate.Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.GetEmailPrincipal();
            var address = _mapper.Map<ShippingAddress>(orderDto.ShippedToAddressDto);

            var order = await _orderService
                .CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order is null)
                return BadRequest(new ApiResponse(400, "Creating Order Failed"));
            return Ok(order);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderDetailsByIdAsync(int id)
        {
            var email = HttpContext.User.GetEmailPrincipal();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order is null)
                return NotFound(new ApiResponse(404));
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return Ok(mappedOrder);
        }
        [HttpGet("GetAllUser'sOrdersDetails")]
        public async Task<ActionResult<IList<OrderDetailsDto>>> GetAllOrdersDetailsAsync()
        {
            var email = HttpContext.User.GetEmailPrincipal();
            var orders = await _orderService.GetOrdersByUserEmailAsync(email);
            if (orders is null)
                return NotFound(new ApiResponse(404));
            var mappedOrders = _mapper.Map<IList<OrderDetailsDto>>(orders);
            return Ok(mappedOrders);
        }

        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<IList<DeliveryMethod>>> GetDeliveryMethodsAsync()
        => Ok(await _orderService.GetDeliveryMethodsAsync());



    }
}
