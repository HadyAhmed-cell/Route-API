using System.ComponentModel.DataAnnotations;

namespace RouteApi.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string Product { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "price must be greater than zero")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "quantity must be greater than zero")]


        public int Quantity { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }
    }
}