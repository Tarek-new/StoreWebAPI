using AutoMapper;
using Core.Enitities;
using Core.Enitities.Identity;
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



        }
    }
}
