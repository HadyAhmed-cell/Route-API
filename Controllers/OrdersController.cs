using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.BLL.Interfaces;
using Route.DAL.Entities.Order;
using RouteApi.Dto;
using RouteApi.Errors;
using System.Security.Claims;

namespace RouteApi.Controllers
{

    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("PlaceOrder")]
        public async Task<ActionResult<Order>> PlaceOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order is null)
            {
                return BadRequest(new ApiResponse(400, "A Problem occured while placing order"));
            }
            return Ok(order);
        }

        [HttpGet("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<UserOrderDto>>> GetOrdersForUsers()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForUserASync(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<UserOrderDto>>(orders));
        }

        [HttpGet("GetOrderByIdForUser/{id}")]
        public async Task<ActionResult<UserOrderDto>> GetOrderByIdForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order is null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<Order, UserOrderDto>(order));

        }

        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DelieveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

            return Ok(deliveryMethods);
        }
    }
}
