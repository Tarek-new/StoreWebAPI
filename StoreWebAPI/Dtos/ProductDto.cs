using Core.Enitities;

namespace StoreWebAPI.Dtos
{
    public class ProductDto: BaseDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }

    }
}
