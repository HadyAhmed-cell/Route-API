using Microsoft.AspNetCore.Identity;
using Route.DAL.Entities.Identity;

namespace Route.BLL.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}
