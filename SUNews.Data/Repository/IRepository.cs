namespace SUNews.Data.Repository
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;

        void Add<T>(T entity) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Save();

        Task SaveAsync();
    }
}
