using Core.Enitities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderWithItemSpecifications : BaseSpecifcation<Order>
    {
        //List of Orders
        public OrderWithItemSpecifications(string buyerEmail) : base(Order => Order.BuyerEmail == buyerEmail)
        {
            AddInclude(Order => Order.OrderItems);
            AddInclude(Order => Order.DeliveryMethod);
            AddOrderByDesc(Order => Order.OrderDate);
        }

        //One Order
        public OrderWithItemSpecifications(string buyerEmail, int id) : base(Order => Order.BuyerEmail == buyerEmail && Order.Id == id)
        {
            AddInclude(Order => Order.OrderItems);
            AddInclude(Order => Order.DeliveryMethod);
        }
    }
}
