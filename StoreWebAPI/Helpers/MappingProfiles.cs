using AutoMapper;
using Core.Enitities;
using Core.Enitities.Identity;
using Core.Enitities.OrderAggregate;
using StoreWebAPI.Dtos;

namespace StoreWebAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, option => option.MapFrom(scr => scr.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, option => option.MapFrom(scr => scr.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<ProductUrlResolver>());
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(scr => scr.DeliveryMethod.ShortName))
                 .ForMember(dest => dest.ShippingFees, option => option.MapFrom(scr => scr.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, option => option.MapFrom(scr => scr.ItemOrder.ProductName))
            .ForMember(dest => dest.ProductId, option => option.MapFrom(scr => scr.ItemOrder.ProductItemId))
            .ForMember(dest => dest.PictureUrl, option => option.MapFrom(scr => scr.ItemOrder.PictureUrl))
            .ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderItemUrlResolver>());







        }
    }
}
