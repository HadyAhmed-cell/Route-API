namespace Route.DAL.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string Id { get; set; }

        public int? DelieveryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public string PaymentIntentId { get; set; }

        public string CLientSecret { get; set; }


    }
}
