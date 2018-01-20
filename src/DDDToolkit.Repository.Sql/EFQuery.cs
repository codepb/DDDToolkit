using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DDDToolkit.Core.Interfaces;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using DDDToolkit.Utilities.Extensions;
using DDDToolkit.Core.Querying;

namespace DDDToolkit.Repository.Sql
{
    internal class EFQuery<T, TId> : IAsyncQuery<T, TId> 
        where T : class, IAggregateRoot<TId>
    {
        private readonly Expression<Func<T, bool>> _expression;

        internal EFQuery(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public IAsyncQuery<T, TId> Where(Expression<Func<T, bool>> query)
        {
            if(_expression == null)
            {
                return new EFQuery<T, TId>(query);
            }
            var body = Expression.AndAlso(_expression.Body, query.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(body, _expression.Parameters[0]);
            return new EFQuery<T, TId>(lambda);
        }

        public Task<IReadOnlyCollection<T>> ExecuteOn(IQueryable<T> queryable)
            => queryable.Where(_expression).ToListAsync().ToReadOnlyCollection();
    }

    internal static class EFQuery
    {
        public static EFQuery<T, TId> Where<T, TId>(Expression<Func<T, bool>> query)
            where T : class, IAggregateRoot<TId>
            => new EFQuery<T, TId>(query);
    }
}