using System.ComponentModel.DataAnnotations;

namespace StoreWebAPI.Dtos
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        [EmailAddress]
        public string BuyerEmail { get; set; }
        public ShippingAddressDto ShippedToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingFees { get; set; }
        public string OrderStatus { get; set; }

        public decimal Total { get; set; }

    }
}
