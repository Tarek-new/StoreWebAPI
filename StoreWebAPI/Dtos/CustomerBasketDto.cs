using Core.Enitities;
using System.ComponentModel.DataAnnotations;

namespace StoreWebAPI.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public int? DeliveryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItem> BasketItems { get; set; }
    }
}
