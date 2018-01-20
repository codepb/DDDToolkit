using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DDDToolkit.Core.Repositories;
using DDDToolkit.Core.Interfaces;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using DDDToolkit.Utilities.Extensions;

namespace DDDToolkit.Repository.Sql
{
    public class Query<T, TId> : IQuery<T, TId> 
        where T : class, IAggregateRoot<TId>
    {
        private readonly Expression<Func<T, bool>> _expression;

        public Query()
        {

        }

        internal Query(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public IQuery<T, TId> Where(Expression<Func<T, bool>> query)
        {
            if(_expression == null)
            {
                return new Query<T, TId>(query);
            }
            var body = Expression.AndAlso(_expression.Body, query.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(body, _expression.Parameters[0]);
            return new Query<T, TId>(lambda);
        }

        public Task<IReadOnlyCollection<T>> Execute(IQueryable<T> queryable)
            => queryable.ToListAsync().ToReadOnlyCollection();
    }
}