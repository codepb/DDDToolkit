using DDDToolkit.Core;
using DDDToolkit.Core.Interfaces;
using DDDToolkit.Core.Repositories;
using DDDToolkit.EntityFramework.Extensions;
using DDDToolkit.Querying;
using DDDToolkit.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql
{
    public class ReadableRepository<T, TId, TContext>
        : EntityFrameworkRepositoryBase<T, TContext>
        , IReadableRepository<T, TId>
        where T : class, IAggregateRoot<TId>
        where TContext : DbContext
    {
        public ReadableRepository(TContext dbContext) : base(dbContext) { }

        protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> query) => query.IncludeEverything();
        protected virtual IQueryable<T> Queryable => ApplyIncludes(Set.AsNoTracking());

        public Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken = default(CancellationToken)) => Set.AsNoTracking().ToListAsync(cancellationToken).ToReadOnlyCollection();

        public Task<T> GetById(TId id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var eId = Expression.Constant(id, typeof(TId));
            var eEntity = Expression.Parameter(typeof(T), "e");
            var eEntityId = Expression.Property(eEntity, nameof(Entity<TId>.Id));
            var eEquals = Expression.Equal(eEntityId, eId);
            var eLambda = Expression.Lambda<Func<T, bool>>(eEquals, eEntity);

            return Queryable.FirstOrDefaultAsync(eLambda, cancellationToken);
        }

        private Task<IReadOnlyCollection<T>> ExecuteQueryOnQueryable(Expression<Func<T, bool>> query, IQueryable<T> queryable, CancellationToken cancellationToken)
            => queryable.Where(query).ToReadOnlyCollectionAsync(cancellationToken);

        public Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteQueryOnQueryable(query, Set.AsNoTracking(), cancellationToken);

        public Task<IReadOnlyCollection<T>> Query(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken))
            => Query(query.AsExpression(), cancellationToken);

        public Task<IReadOnlyCollection<T>> QueryWithChildren(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default(CancellationToken))
            => ExecuteQueryOnQueryable(query, Queryable, cancellationToken);

        public Task<IReadOnlyCollection<T>> QueryWithChildren(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken))
            => QueryWithChildren(query.AsExpression(), cancellationToken);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default(CancellationToken))
            => Queryable.FirstOrDefaultAsync(query, cancellationToken);

        public Task<T> FirstOrDefault(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken))
            => FirstOrDefault(query.AsExpression(), cancellationToken);
    }
}
