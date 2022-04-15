namespace SUNews.Services.Constants
{
    public static class MessageConstant
    {
        public const string ErrorMessage = "ErrorMessage";
        public const string SuccessMessage = "SuccessMessage";
        public const string WarningMessage = "WarningMessage";

        // Article service messages
        public const string ArticleNotFound = "Sorry article not found";


        // Category service messages
        public const string CategoryNotFound = "Sorry Category's articles not found";

        // Comment service messages
        public const string ErrorUserID = "User with this ID can't found.";
        public const string ErrorArticleID = "Article with this ID can't found.";
        public const string ErrorCommentID = "Article with this ID can't found.";
    }
}
