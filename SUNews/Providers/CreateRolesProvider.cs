using Microsoft.AspNetCore.Identity;
using SUNews.Data.Models;

namespace SUNews.Providers
{
    public class CreateRolesProvider
    {
        private readonly IServiceProvider serviceProvider;

        public CreateRolesProvider(IServiceProvider _serviceProvider)
        {
            this.serviceProvider = _serviceProvider;
        }

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            //Adding Admin Role 
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database 
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                await RoleManager.CreateAsync(new IdentityRole("User"));
            }

            //Assign Admin role to the main User here we have given our newly registered
            //login id for Admin management
            User user = await UserManager.FindByEmailAsync("admin@abv.bg");
            var User = new User();
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
