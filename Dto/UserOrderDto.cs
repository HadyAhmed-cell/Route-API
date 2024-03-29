﻿using Route.DAL.Entities.Order;

namespace RouteApi.Dto
{
    public class UserOrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryCost { get; set; }

        public string Status { get; set; }

        public IReadOnlyList<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
    }
}
