namespace SUNews.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static SUNews.Data.DataConstants;

    public class UserEditViewModel
    {
        public UserEditViewModel()
        {

        }

        public UserEditViewModel(User user)
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

        [Required]
        [StringLength(IdMaxLength)]
        public string Id { get; set; }

        [Display(Name = "Username")]
        //[StringLength(FirstAndLastNameLength)]
        public string? UserName { get; set; }

        //[Required]
        [Display(Name = "First name")]
        //[StringLength(FirstAndLastNameLength, MinimumLength = 3)]
        public string? FirstName { get; set; }

        //[Required]
        [Display(Name = "Last name")]
        //[StringLength(FirstAndLastNameLength, MinimumLength = 3)]
        public string? LastName { get; set; }

        [Display(Name = "Email address")]
        public string? Email { get; set; }

        [Display(Name = "Phone number")]
        //[RegularExpression("^[0-9]", ErrorMessage = "Please enter only digits between 0 - 9.")]
        public string? PhoneNumber { get; set; }

        public bool? LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        [Display(Name = "User roles")]
        public ICollection<string> Roles { get; set; } = new List<string>();

        // This must to be here to list Categories in View!
        public IEnumerable<SelectListItem> RolesList { get; set; } = new List<SelectListItem>();
    }
}