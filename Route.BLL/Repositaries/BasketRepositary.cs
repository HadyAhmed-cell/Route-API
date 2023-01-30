using Route.BLL.Interfaces;
using Route.DAL.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Route.BLL.Repositaries
{
    public class BasketRepositary : IBasketRepositary
    {
        private readonly IDatabase _database;
        public BasketRepositary(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        => await _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket> GetBaskteAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBaskteAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket));
            if (!created)
            {
                return null;

            }
            return await GetBaskteAsync(basket.Id);
        }
    }
}
