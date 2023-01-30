using AutoMapper;
using Route.DAL.Entities.Order;
using RouteApi.Dto;

namespace RouteApi.Helper
{

    public class OrderUrlReslover : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderUrlReslover(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return $"{_configuration["ApiUrl"]}{source.ItemOrdered.PictureUrl}";
            }

            return null;
        }
    }

}
