namespace SUNews.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SUNews.Data.Context;
    using System.Linq;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> 
        where T : class
    {
		private readonly SUNewsDbContext context;

		public Repository(SUNewsDbContext context)
		{
			this.context = context;
		}

		public IQueryable<T> All()
		{
			return context.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			EntityEntry entry = context.Entry(entity);

			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Added;
			}
			else
			{
				await context.Set<T>().AddAsync(entity);
			}
		}

		public void Update(T entity)
		{
			EntityEntry entry = context.Entry(entity);
			if (entry.State == EntityState.Detached)
			{
				context.Set<T>().Attach(entity);
			}

			entry.State = EntityState.Modified;
		}

		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}

		void IRepository<T>.Add(T entity)
		{
			EntityEntry entry = context.Entry(entity);

			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Added;
			}
			else
			{
				context.Set<T>().Add(entity);
			}
		}

		void IRepository<T>.Save()
		{
			context.SaveChanges();
		}
	}
}
