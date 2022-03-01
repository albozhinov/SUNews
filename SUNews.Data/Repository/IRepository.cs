namespace SUNews.Data.Repository
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        void Add(T entity);

        Task AddAsync(T entity);

        void Update(T entity);

        void Save();

        Task SaveAsync();
    }
}
