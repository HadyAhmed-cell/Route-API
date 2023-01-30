using Route.BLL.Interfaces;
using Route.BLL.OrderSpecification;
using Route.DAL.Entities;
using Route.DAL.Entities.Order;

namespace Route.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositary _basketRepositary;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepositary basketRepositary, IUnitOfWork unitOfWork)
        {
            _basketRepositary = basketRepositary;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepositary.GetBaskteAsync(basketId);
            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repositary<Product>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repositary<DelieveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //check to see if order exists => TODO

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
            _unitOfWork.Repositary<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }
            return order;

        }

        public async Task<IReadOnlyList<DelieveryMethod>> GetDeliveryMethodsAsync()
        => await _unitOfWork.Repositary<DelieveryMethod>().GetAllAsync();

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(orderId, buyerEmail);

            return await _unitOfWork.Repositary<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserASync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(buyerEmail);

            return await _unitOfWork.Repositary<Order>().GetAllWithSpecAsync(spec);
        }
    }
}
