using System.ComponentModel.DataAnnotations;

namespace RouteApi.Dto
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]


        public string Country { get; set; }
        [Required]


        public string City { get; set; }


        public string Street { get; set; }
        public string ZipCode { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public string Email { get; set; }


    }
}
