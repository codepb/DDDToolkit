using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using DDDToolkit.EntityFramework.Extensions;
using DDDToolkit.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql
{
    public class ReadableRepository<T, TId, TContext>
        : EntityFrameworkRepositoryBase<T, TContext>
        , IReadableRepository<T, TId>
        where T : AggregateRoot<TId>
        where TContext : DbContext
    {
        public ReadableRepository(TContext dbContext) : base(dbContext) { }

        protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> query) => query.IncludeEverything();
        protected virtual IQueryable<T> Queryable => ApplyIncludes(Set.AsNoTracking());

        public Task<IReadOnlyCollection<T>> GetAll() => Set.ToListAsync().AsTaskOf<List<T>, IReadOnlyCollection<T>>();

        public Task<T> GetById(TId id)
        {
            var eId = Expression.Constant(id, typeof(TId));
            var eEntity = Expression.Parameter(typeof(T), "e");
            var eEntityId = Expression.Property(eEntity, nameof(Entity<TId>.Id));
            var eEquals = Expression.Equal(eEntityId, eId);
            var eLambda = Expression.Lambda<Func<T, bool>>(eEquals, eEntity);

            return Queryable.FirstOrDefaultAsync(eLambda);
        }

        public Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query)
        {
            return Set.Where(query).ToListAsync().AsTaskOf<List<T>, IReadOnlyCollection<T>>();
        }
    }
}
