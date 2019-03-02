using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core;
using DDDToolkit.Core.Interfaces;
using DDDToolkit.EntityFramework.Extensions;
using FluentQueries;
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

        private IQueryable<T> SetupPaging(PagingOptions pagingOptions, IQueryable<T> queryable)
        {
            if(pagingOptions == null)
            {
                return queryable;
            }
            return queryable.Skip((pagingOptions.Page - 1) * pagingOptions.AmountPerPage).Take(pagingOptions.AmountPerPage);
        }

        private Task<IReadOnlyCollection<T>> ExecuteQueryOnQueryable(Expression<Func<T, bool>> query, IQueryable<T> queryable, CancellationToken cancellationToken, PagingOptions pagingOptions = null)
        {
            var queryDefinition = queryable.Where(query);

            if(pagingOptions != null)
            {
                queryDefinition = SetupPaging(pagingOptions, queryDefinition);
            }
            
            return queryDefinition.ToReadOnlyCollectionAsync(cancellationToken);
        }
        private static IQueryable<T> SetupIncludes(string[] pathsToInclude, IQueryable<T> query)
        {
            if (pathsToInclude != null)
            {
                foreach (var path in pathsToInclude)
                {
                    query = query.Include(path);
                }
            }

            return query;
        }

        private IQueryable<T> SetupQueryable(string[] onlyIncludePaths)
        {
            return onlyIncludePaths == null ? Queryable : SetupIncludes(onlyIncludePaths, Set.AsNoTracking());
        }

        public Task<T> GetById(TId id, string[] onlyIncludePaths, CancellationToken cancellationToken = default(CancellationToken))
        {
            var eId = Expression.Constant(id, typeof(TId));
            var eEntity = Expression.Parameter(typeof(T), "e");
            var eEntityId = Expression.Property(eEntity, nameof(Entity<TId>.Id));
            var eEquals = Expression.Equal(eEntityId, eId);
            var eLambda = Expression.Lambda<Func<T, bool>>(eEquals, eEntity);

            var queryable = SetupQueryable(onlyIncludePaths);
            return queryable.FirstOrDefaultAsync(eLambda, cancellationToken);
        }

        public Task<IReadOnlyCollection<T>> Query(QueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryable = SetupQueryable(options?.OnlyIncludePaths);
            return SetupPaging(options?.PagingOptions, queryable).ToReadOnlyCollectionAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query, QueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryable = SetupQueryable(options?.OnlyIncludePaths);
            return ExecuteQueryOnQueryable(query, queryable, cancellationToken, options?.PagingOptions);
        }

        public Task<IReadOnlyCollection<T>> Query(IQuery<T> query, QueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Query(query.AsExpression(), options, cancellationToken);
        }

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> query, string[] onlyIncludePaths = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryable = SetupQueryable(onlyIncludePaths);
            return queryable.FirstOrDefaultAsync(query, cancellationToken);
        }

        public Task<T> FirstOrDefault(IQuery<T> query, string[] onlyIncludePaths = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FirstOrDefault(query.AsExpression(), onlyIncludePaths, cancellationToken);
        }
    }
}
