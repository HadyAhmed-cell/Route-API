using System.ComponentModel.DataAnnotations;

namespace RouteApi.Dto
{
    public class LoginDto
    {
        [Required]

        public string Password { get; set; }
        [Required]

        public string Email { get; set; }
    }
}
