using DDDToolkit.Core.Interfaces;
using DDDToolkit.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql
{
    public class WritableRepository<T, TId, TContext>
        : EntityFrameworkRepositoryBase<T, TContext>
        , IWritableRepository<T, TId>
        where T : class, IAggregateRoot<TId>
        where TContext : DbContext
    {
        public WritableRepository(TContext dbContext) : base(dbContext) { }

        public virtual Task Add(T entity)
        {
            Set.Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task Update(T entity)
        {
            var trackedEntity = DbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id.Equals(entity.Id));
            if(trackedEntity != null)
            {
                trackedEntity.State = EntityState.Detached;
            }
            Set.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task Remove(TId id)
        {
            var current = await Set.FirstOrDefaultAsync(e => e.Id.Equals(id));
            Set.Remove(current);
        }
    }
}
