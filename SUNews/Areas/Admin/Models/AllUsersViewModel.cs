namespace SUNews.Areas.Admin.Models
{
    using SUNews.Data.Models;

    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public AllUsersViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            LockoutEnabled = user.LockoutEnabled;
            LockoutEnd = user.LockoutEnd;


            if (String.IsNullOrWhiteSpace(user.FirstName) && String.IsNullOrWhiteSpace(user.LastName))
            {
                Name = user.Email;
            }
            else
            {
                Name = user.FirstName + " " + user.LastName;
            }
        }
    }
}
