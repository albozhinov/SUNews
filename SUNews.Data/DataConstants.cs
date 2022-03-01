namespace SUNews.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class DataConstants
    {
        //User
        //public const int IdMaxLength = 40;
        //public const int DefaultMaxLength20 = 20;

        //public const int UserMinUsername = 5;
        //public const int UserMinPassword = 5;
        //public const int UserMaxHashedPassword = 64;
        //public const int UserMinEmail = 10;
        //public const int UserMaxEmail = 60;
        //public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        //Article        
        public const int ArticleTitleMinLength = 5;
        public const int ArticleTitleMaxLength = 100;
        public const int ArticleContentMinLength = 20;
        public const int ArticleContentMaxLength = 1000;
        public const double ArticleRatingMin = 0;
        public const double ArticleRatingMax = 5;


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
