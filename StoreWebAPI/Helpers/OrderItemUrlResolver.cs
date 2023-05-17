using AutoMapper;
using Core.Enitities.OrderAggregate;
using StoreWebAPI.Dtos;

namespace StoreWebAPI.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>

    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrder.PictureUrl))
                return _configuration["ApiURL"] + source.ItemOrder.PictureUrl;
            return null;
        }
    }

}
