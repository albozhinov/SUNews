namespace SUNews.Providers
{
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    public interface IUserManager<T> where T : class
    {
        Task<IdentityResult> SetLockoutEndDateAsync(T user, DateTimeOffset? lockoutEnd);

        Task<IdentityResult> SetLockoutEnabledAsync(T user, bool enabled);

        Task<IdentityResult> AddPasswordAsync(T user, string password);

        Task<IdentityResult> AddToRoleAsync(T user, string role);

        Task<IdentityResult> RemoveFromRoleAsync(T user, string role);

        Task<IList<string>> GetRolesAsync(T user);

        Task<string> GetEmailAsync(T user);

        Task<IdentityResult> RemovePasswordAsync(T user);

        Task<T> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        Task<string> GetUserIdAsync(T user);

        IQueryable<T> Users { get; }

        IList<IPasswordValidator<T>> PasswordValidators { get; }

        UserManager<T> Instance { get; }
    }
}
