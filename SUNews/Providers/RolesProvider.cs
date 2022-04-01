using Microsoft.AspNetCore.Identity;
using SUNews.Data.Models;
using SUNews.Services.Constants;
using System.Security.Claims;

namespace SUNews.Providers
{
    public class RolesProvider : IRolesProvider
    {
        private readonly IRoleManager<IdentityRole> roleManager;
        private readonly IUserManager<User> userManager;

        public RolesProvider(IRoleManager<IdentityRole> _roleManager, IUserManager<User> _userManager)
        {
            this.roleManager = _roleManager;
            this.userManager = _userManager;
        }

        public Task<(string, string)> AddUserToRoles(ClaimsPrincipal user, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<(string, string)> CreateRole(string role)
        {
            (string, string) message;
            //var user = await userManager.GetUserAsync(loggedUser);
            //var userRoles = await userManager.GetRolesAsync(user);

            //if (userRoles.Contains(role))
            //{
            //    message.Item1 = MessageConstant.WarningMessage;
            //    message.Item2 = $"{user.Email} already added to this role!";
            //    return message;
            //}

            //Adding Role 
            var roleCheck = await roleManager.RoleExistsAsync(role);
            if (roleCheck)
            {
                message.Item1 = MessageConstant.WarningMessage;
                message.Item2 = $@"""{role}"" role already exists!";

                return message;
            }

            //create the roles and seed them to the database 
            await roleManager.CreateAsync(new IdentityRole(role));

            //Assign Role to the main User here we have given our newly registered
            //login id for Admin management            
            //await userManager.AddToRoleAsync(user, role);

            message.Item1 = MessageConstant.SuccessMessage;
            message.Item2 = @$"Successfully created role ""{role}""!";

            return message;
        }
    }
}
