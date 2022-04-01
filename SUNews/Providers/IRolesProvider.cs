namespace SUNews.Providers
{
    using System.Security.Claims;

    public interface IRolesProvider
    {
        Task<(string, string)> CreateRole(string role);

        Task<(string, string)> AddUserToRoles(ClaimsPrincipal user, string role);

    }
}
