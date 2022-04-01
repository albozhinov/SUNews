namespace SUNews.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        public ICollection<string> Roles { get; set; } = new List<string>();

        // This must to be here to list Categories in View!
        public IEnumerable<SelectListItem> RolesList { get; set; } = new List<SelectListItem>();
    }
}
