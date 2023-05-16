using Core.Enitities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Jennie",
                    Email = "jennie.nichols@example.com",
                    UserName = "yellowpeacock117",
                    Address = new Address
                    {
                        FirstName = "Jennie",
                        LastName = "Nichols",
                        Street = "Valwood Pkwy",
                        City = "Billings",
                        State = "Michigan",
                        ZipCode = "63104"
                    }
                };
                await userManager.CreateAsync(user, "Password@123");
            }

        }
    }
}
