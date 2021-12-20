using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.SeedData
{
    public class AppIdentityDbContextSeed
    {
            public static async Task SeedUserAsync(UserManager<AppUser> userManager)
            {
                if(!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        DisplayName = "Ahmed",
                        Email = "A7mdsaber99@gmail.com",
                        UserName = "Ahmed",
                        Address = new Address
                        {
                            Country = "Egypt",
                            City = "Cairo",
                            Street = "10 The Street",
                            ZipCode = "9031",

                        }
                    };
                    var response = await userManager.CreateAsync(user, "123456@Ahmed");
                }
            }


    }
}