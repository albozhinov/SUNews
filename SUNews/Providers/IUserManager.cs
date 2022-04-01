namespace SUNews.Providers
{
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    public interface IUserManager<T> where T : class
    {
        Task<T> FindByIdAsync(string userId);
        Task<IdentityResult> SetLockoutEndDateAsync(T user, DateTimeOffset? lockoutEnd);

        Task<IdentityResult> SetLockoutEnabledAsync(T user, bool enabled);

        Task<bool> IsLockedOutAsync(T user);

        Task<IdentityResult> AddPasswordAsync(T user, string password);

        Task<IdentityResult> AddToRoleAsync(T user, string role);

        Task<IdentityResult> AddToRolesAsync(T user, IEnumerable<string> roles);

        Task<IdentityResult> RemoveFromRoleAsync(T user, string role);

        Task<IdentityResult> RemoveFromRolesAsync(T user, IEnumerable<string> roles);

        Task<IList<string>> GetRolesAsync(T user);

        Task<string> GetEmailAsync(T user);

        Task<IdentityResult> RemovePasswordAsync(T user);

        Task<T> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        Task<string> GetUserIdAsync(T user);

        Task<IList<T>> GetUsersInRoleAsync(string role);

        IQueryable<T> Users { get; }

        IList<IPasswordValidator<T>> PasswordValidators { get; }

        UserManager<T> Instance { get; }
    }
}
