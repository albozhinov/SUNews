﻿using SUNews.Data.Models;

namespace SUNews.Models
{
    public class AllArticlesViewModel
    {
        public Guid Id { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public int Likes { get; init; }

        public string AuthorName { get; init; }

        public DateTime DateOfCreation { get; init; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public AllArticlesViewModel(Article model)
        {
            Id = model.Id;
            Title = model.Title;
            ImageUrl = model.ImageUrl;
            AuthorName = model.Author.Name;
            DateOfCreation = model.DateOfCreation;
            //Categories = model.Categories.Select(c => new CategoryViewModel(c.Category)).ToList();
            Likes = model.LikeCount ?? 0;
        }
    }
}
