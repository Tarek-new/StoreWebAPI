namespace Core.Enitities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public int Id { get; set; }
        public ProductItemOrder ItemOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}