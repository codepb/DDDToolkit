using System.Collections.Generic;
using DDDToolkit.Core.Interfaces;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace DDDToolkit.Core.Querying
{
    public class Query<T, TId> : IQuery<T, TId> 
        where T : class, IAggregateRoot<TId>
    {
        private readonly Expression<Func<T, bool>> _expression = null;

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

        public IReadOnlyCollection<T> Execute(IQueryable<T> queryable)
            => queryable.ToList().AsReadOnly();
    }

    public static class Query
    {
        public static Query<T, TId> Where<T, TId>(Expression<Func<T, bool>> query)
            where T : class, IAggregateRoot<TId>
            => new Query<T, TId>(query);
    }
}