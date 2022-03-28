using SUNews.Data.Models;

namespace SUNews.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public ICollection<AllArticlesViewModel> Articles { get; set; } = new List<AllArticlesViewModel>();

        public CategoryViewModel(Category model)
        {
            Id = model.Id;
            Name = model.Name;
            // Трябва да добавим DTO модели в SUNew.Service за да улесним подаването на данни от
            // Service layer-a към Web-a !!!
            Articles = model.Articles.Select(a => new AllArticlesViewModel(a.Article)).ToList();
        }
    }
}
