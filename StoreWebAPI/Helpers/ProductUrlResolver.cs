using AutoMapper;
using Core.Enitities;
using StoreWebAPI.Dtos;

namespace StoreWebAPI.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) 
                return _configuration["ApiURL"] + source.PictureUrl;
            return null;
        }
    }
}
