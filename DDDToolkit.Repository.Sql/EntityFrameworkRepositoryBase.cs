using Microsoft.EntityFrameworkCore;

namespace DDDToolkit.Repository.Sql
{
    public abstract class EntityFrameworkRepositoryBase<T, TContext>
        where T : class
        where TContext : DbContext
    {
        protected TContext DbContext { get; }
        protected DbSet<T> Set => DbContext.Set<T>();

        protected EntityFrameworkRepositoryBase(TContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
