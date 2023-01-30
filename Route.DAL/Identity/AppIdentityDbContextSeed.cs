using Microsoft.AspNetCore.Identity;
using Route.DAL.Entities.Identity;

namespace Route.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Hady Ahmed",
                    UserName = "hady57",
                    Email = "hady57@gmail.com",
                    PhoneNumber = "01062832107",
                    Address = new Address()
                    {
                        FirstName = "ahmed",
                        LastName = "lol",
                        Country = "egypt",
                        City = "cairo",
                        Street = "10 tahrir",
                        ZipCode = "11133"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
