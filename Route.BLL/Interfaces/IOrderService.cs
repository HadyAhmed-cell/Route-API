using Route.DAL.Entities.Order;

namespace Route.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserASync(string buyerEmail);

        Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail);

        Task<IReadOnlyList<DelieveryMethod>> GetDeliveryMethodsAsync();
    }
}
