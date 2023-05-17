using Core.Enitities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress);
        Task<IList<Order>> GetOrdersByUserEmailAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
