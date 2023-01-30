namespace Route.DAL.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail,
            Address shipToAddress,
            DelieveryMethod deliveryMethod,
             IReadOnlyList<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public Address ShipToAddress { get; set; }
        public DelieveryMethod DeliveryMethod { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public IReadOnlyList<OrderItem> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal()
            => SubTotal = DeliveryMethod.Cost;
    }
}