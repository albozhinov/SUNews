namespace SUNews.Areas.Admin.Models
{
using System.ComponentModel.DataAnnotations;

    public class CreateRoleViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression("^[A-Za-z]+$")]
        public string Role { get; set; }
    }
}
