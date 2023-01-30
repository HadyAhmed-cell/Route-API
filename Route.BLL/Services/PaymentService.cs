using Microsoft.Extensions.Configuration;
using Route.BLL.Interfaces;
using Route.BLL.OrderSpecification;
using Route.DAL.Entities;
using Route.DAL.Entities.Order;
using Stripe;
using Product = Route.DAL.Entities.Product;

namespace Route.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepositary _basket;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, IBasketRepositary basket
            , IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _basket = basket;
            _configuration = configuration;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var basket = await _basket.GetBaskteAsync(basketId);

            if (basket is null)
            {
                return null;
            }
            var shippingPrice = 0m;
            if (basket.DelieveryMethod.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repositary<DelieveryMethod>().GetByIdAsync(basket.DelieveryMethod.Value);
                shippingPrice = deliveryMethod.Cost;
            }
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repositary<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                    item.Price = productItem.Price;


            }
            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => (long)i.Quantity * ((long)i.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.CLientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => (long)i.Quantity * ((long)i.Price * 100)) + ((long)shippingPrice * 100),

                };
                await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            basket.ShippingPrice = shippingPrice;
            await _basket.UpdateBaskteAsync(basket);

            return basket;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(paymentIntentId);
            var order = await _unitOfWork.Repositary<Order>().GetEntityWithSpec(spec);
            if (order is null)
            {
                return null;
            }

            order.Status = OrderStatus.PaymentFailed;
            _unitOfWork.Repositary<Order>().Update(order);

            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(paymentIntentId);
            var order = await _unitOfWork.Repositary<Order>().GetEntityWithSpec(spec);
            if (order is null)
            {
                return null;
            }

            order.Status = OrderStatus.Pending;
            _unitOfWork.Repositary<Order>().Update(order);

            await _unitOfWork.Complete();
            return order;
        }
    }
}
