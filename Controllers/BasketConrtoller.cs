using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.BLL.Interfaces;
using Route.DAL.Entities;
using RouteApi.Dto;

namespace RouteApi.Controllers
{

    public class BasketConrtoller : BaseApiController
    {
        private readonly IBasketRepositary _basket;
        private readonly IMapper _mapper;

        public BasketConrtoller(IBasketRepositary basket, IMapper mapper)
        {
            _basket = basket;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketId(string id)
        {
            var basket = await _basket.GetBaskteAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updatedBasket = await _basket.UpdateBaskteAsync(customerBasket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<bool> DeleteBasket(string id)
            => await _basket.DeleteBasketAsync(id);


    }
}
