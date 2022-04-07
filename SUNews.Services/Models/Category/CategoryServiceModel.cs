using SUNews.Data.Models;

namespace SUNews.Services.Models
{
    public class CategoryServiceModel
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public ICollection<AllArticlesServiceModel> Articles { get; set; } = new List<AllArticlesServiceModel>();

        public CategoryServiceModel(Category model)
        {
            Id = model.Id;
            Name = model.Name;
            // Трябва да добавим DTO модели в SUNew.Service за да улесним подаването на данни от
            // Service layer-a към Web-a !!!
            Articles = model.Articles.Select(a => new AllArticlesServiceModel(a.Article)).ToList();
        }
    }
}
