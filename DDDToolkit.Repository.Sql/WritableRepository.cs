using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql
{
    public class WritableRepository<T, TId, TContext>
        : EntityFrameworkRepositoryBase<T, TContext>
        , IWritableRepository<T, TId>
        where T : AggregateRoot<TId>
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
