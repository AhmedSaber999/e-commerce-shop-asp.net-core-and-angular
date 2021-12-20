using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtentions
    {
        public static AppUser FindByEmailWithAddress(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            // var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return input.Users.Where(x => x.Email == email).Include(x => x.Address).FirstOrDefault();
        }
        public static AppUser FindByEmailFromClaimsPrincipleWithAddress(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            // var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return input.Users.Where(x => x.Email == email).Include(x => x.Address).FirstOrDefault();
        }
    }
}