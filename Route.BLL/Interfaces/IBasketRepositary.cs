using Route.DAL.Entities;

namespace Route.BLL.Interfaces
{
    public interface IBasketRepositary
    {
        Task<CustomerBasket> GetBaskteAsync(string basketId);
        Task<CustomerBasket> UpdateBaskteAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
