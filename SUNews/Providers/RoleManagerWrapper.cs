namespace SUNews.Providers
{
    using Microsoft.AspNetCore.Identity;

    public class RoleManagerWrapper<T> : IRoleManager<T> where T : class
    {
        private readonly RoleManager<T> _roleManager;

        public RoleManagerWrapper(RoleManager<T> roleManager)
        {
            this._roleManager = roleManager;
        }

        public IQueryable<T> Roles => this._roleManager.Roles;

        public RoleManager<T> Istance => this._roleManager;

        public async Task<IdentityResult> CreateAsync(T role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await this._roleManager.RoleExistsAsync(roleName);
        }
    }
}
