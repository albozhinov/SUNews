﻿namespace SUNews.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class CommentReview
    {
        public int Id { get; init; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
