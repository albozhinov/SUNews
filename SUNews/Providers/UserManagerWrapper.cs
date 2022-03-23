namespace SUNews.Providers
{
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    public class UserManagerWrapper<T> : IUserManager<T> where T : class
    {
        private readonly UserManager<T> _userManager;

        public UserManagerWrapper(UserManager<T> userManager)
        {
            this._userManager = userManager;
        }

        public IQueryable<T> Users => this._userManager.Users;

        public IList<IPasswordValidator<T>> PasswordValidators => this._userManager.PasswordValidators;

        public UserManager<T> Instance => this._userManager;

        public async Task<IdentityResult> AddPasswordAsync(T user, string password) =>
                                        await this._userManager.AddPasswordAsync(user, password);

        public async Task<IdentityResult> AddToRoleAsync(T user, string role) =>
                                        await this._userManager.AddToRoleAsync(user, role);

        public async Task<IList<string>> GetRolesAsync(T user) => await this._userManager.GetRolesAsync(user);

        public async Task<T> GetUserAsync(ClaimsPrincipal claimsPrincipal) =>
                                        await this._userManager.GetUserAsync(claimsPrincipal);

        public async Task<string> GetUserIdAsync(T user) => await this.GetUserIdAsync(user);

        public async Task<IdentityResult> RemoveFromRoleAsync(T user, string role) =>
                                        await this._userManager.RemoveFromRoleAsync(user, role);

        public async Task<IdentityResult> RemovePasswordAsync(T user) =>
                                        await this._userManager.RemovePasswordAsync(user);

        public async Task<IdentityResult> SetLockoutEnabledAsync(T user, bool enabled) =>
                                        await this._userManager.SetLockoutEnabledAsync(user, enabled);

        public async Task<IdentityResult> SetLockoutEndDateAsync(T user, DateTimeOffset? lockoutEnd) =>
                                        await this._userManager.SetLockoutEndDateAsync(user, lockoutEnd);

        public async Task<string> GetEmailAsync(T user) => 
                                        await this._userManager.GetEmailAsync(user);
        
    }
}
