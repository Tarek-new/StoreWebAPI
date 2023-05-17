using System.ComponentModel.DataAnnotations;

namespace Core.Enitities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        [EmailAddress]
        public string BuyerEmail { get; set; }
        public ShippingAddress ShippedToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IList<OrderItem> OrderItems { get; set; }

        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;



    }
}
