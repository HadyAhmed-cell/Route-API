using Route.BLL.Specifications;
using Route.DAL.Entities.Order;

namespace Route.BLL.OrderSpecification
{
    public class OrderWithItemsAndDeliveryMethodSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemsAndDeliveryMethodSpecification(int id, string email) : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);

        }
    }
}
