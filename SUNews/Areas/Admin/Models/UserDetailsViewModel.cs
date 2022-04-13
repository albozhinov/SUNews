namespace SUNews.Areas.Admin.Models
{
    using SUNews.Data.Models;

    public class UserDetailsViewModel
    {
        public UserDetailsViewModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            LockoutEnabled = user.LockoutEnabled;
            LockoutEnd = user.LockoutEnd;


            if (String.IsNullOrWhiteSpace(user.FirstName) && String.IsNullOrWhiteSpace(user.LastName))
            {
                UserName = user.Email;
            }
            else
            {
                UserName = user.FirstName + " " + user.LastName;
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
