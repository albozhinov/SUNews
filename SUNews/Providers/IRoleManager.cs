namespace SUNews.Providers
{
    using Microsoft.AspNetCore.Identity;

    public interface IRoleManager<T> where T : class
    {
        Task<bool> RoleExistsAsync(string roleName);

        IQueryable<T> Roles { get; }

        RoleManager<T> Istance { get; }
    }
}
