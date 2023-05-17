namespace StoreWebAPI.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }

        public ShippingAddressDto ShippedToAddressDto { get; set; }
        public int DeliveryMethodId { get; set; }




    }
}
