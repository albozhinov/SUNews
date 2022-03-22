namespace SUNews.Services.Services
{
    using SUNews.Data.Models;
    using SUNews.Services.Contracts;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        public Task<User> EditUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
