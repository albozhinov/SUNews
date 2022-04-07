using SUNews.Services.Models;

namespace SUNews.Models
{
    public class ArticleAndCommentsViewModel
    {
        public DetailsOfArticlesServiceModel DetailsOfArticlesServiceModel { get; set; }

        public CreateCommentViewModel CreateCommentViewModel { get; set; }
    }
}
