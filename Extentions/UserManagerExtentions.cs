using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.DAL.Entities.Identity;
using System.Security.Claims;

namespace RouteApi.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
