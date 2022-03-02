namespace SUNews.Data
{
    public class DataConstants
    {
        //Article        
        public const int ArticleTitleMinLength = 5;
        public const int ArticleTitleMaxLength = 100;
        public const int ArticleContentMinLength = 20;
        public const int ArticleContentMaxLength = 1000;
        public const double ArticleRatingMin = 0;
        public const double ArticleRatingMax = 5;
        public const int ArticleImageUrlMaxLength = 500;

        //Author
        public const int AuthorNameMinLength = 5;
        public const int AuthorNameMaxLength = 40;

        //Comment
        public const int CommentTextMinLength = 6;
        public const int CommentTextMaxLength = 150;

        //Category
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 20;
    }
}
