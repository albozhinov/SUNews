namespace SUNews.Providers
{
    using System.Security.Claims;

    public interface ICreateRolesProvider
    {
        Task<(string, string)> CreateUserRoles(ClaimsPrincipal user, string role);
    }
}
