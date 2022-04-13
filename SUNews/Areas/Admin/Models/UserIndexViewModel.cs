namespace SUNews.Areas.Admin.Models
{
    using SUNews.Data.Models;
    using X.PagedList;

    public class UserIndexViewModel
    {
        public UserIndexViewModel(IEnumerable<User> users, int pageNumber, int pageSize)
        {
            this.Users = users.Select(u => new AllUsersViewModel(u)).ToPagedList(pageNumber, pageSize);
        }

        public string StatusMessage { get; set; }

        public IPagedList<AllUsersViewModel> Users { get; set; }
    }
}
