using Core.Enitities;
using Core.Enitities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress)
        {
            //GetBasket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //GetBasketItems from Product Repository
            var items = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                var ItemOrdered = new ProductItemOrder
                {
                    ProductItemId = item.Id,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl

                };
                var orderItem = new OrderItem
                {
                    ItemOrder = ItemOrdered,
                    Price = product.Price,
                    Quantity = item.Quantity,

                };
                items.Add(orderItem);
            }

            //DeliveryMethod
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(deliveryMethodId);

            // Calculate Price without Shipping fees

            var subPrice = items.Sum(item => item.Price * item.Quantity);

            //ToDoPayment

            //Create Order
            var order = new Order
            {
                BuyerEmail = BuyerEmail,
                SubTotal = subPrice,
                ShippedToAddress = shippingAddress,
                DeliveryMethod = deliveryMethod,
                OrderItems = items,

            };
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Commit();

            if (result <= 0)
                return null;
            await _basketRepository.DeleteBasketAsync(basketId);

            return order;

        }

        public async Task<IList<DeliveryMethod>> GetDeliveryMethodsAsync()
             => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var OrderSpec = new OrderWithItemSpecifications(buyerEmail, id);
            return await _unitOfWork.Repository<Order>().GetBySpecificationsAsync(OrderSpec);

        }

        public async Task<IList<Order>> GetOrdersByUserEmailAsync(string buyerEmail)
        {
            var OrdersSpec = new OrderWithItemSpecifications(buyerEmail);
            return await _unitOfWork.Repository<Order>().GetListBySpecificationsAsync(OrdersSpec);
        }
    }
}
