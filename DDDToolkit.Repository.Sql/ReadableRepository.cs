﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core.Repositories;
using DDDToolkit.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DDDToolkit.Repository.Sql.Extensions;
using System.Linq.Expressions;

namespace DDDToolkit.Repository.Sql
{
    public class ReadableRepository<T, TId, TContext>
        : EntityFrameworkRepositoryBase<T, TContext>
        , IReadableRepository<T, TId>
        where T : AggregateRoot<TId>
        where TContext : DbContext
    {
        protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> query) => query;
        protected virtual IQueryable<T> Queryable => ApplyIncludes(Set.AsNoTracking());

        public ReadableRepository(TContext dbContext) : base(dbContext) { }

        public Task<IReadOnlyCollection<T>> GetAll()
        {
            return Set.ToListAsync().AsTaskOf<List<T>, IReadOnlyCollection<T>>();
        }

        public Task<T> GetById(TId id)
        {
            var eId = Expression.Constant(id, typeof(TId));
            var eEntity = Expression.Parameter(typeof(T), "e");
            var eEntityId = Expression.Property(eEntity, nameof(Entity<TId>.Id));
            var eEquals = Expression.Equal(eEntityId, eId);
            var eLambda = Expression.Lambda<Func<T, bool>>(eEquals, eEntity);

            return Queryable.FirstOrDefaultAsync(eLambda);
        }

        public Task<IReadOnlyCollection<T>> Query(Func<T, bool> query)
        {
            return Task.FromResult(Set.Where(query).ToList()).AsTaskOf<List<T>, IReadOnlyCollection<T>>();
        }
    }
}
