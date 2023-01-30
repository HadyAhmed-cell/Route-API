using System.ComponentModel.DataAnnotations;

namespace RouteApi.Dto
{
    public class CustomerBasketDto
    {
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        [Required]
        public string Id { get; set; }

        public string? DelieveryMethod { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}
