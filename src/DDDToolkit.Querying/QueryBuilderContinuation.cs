using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class QueryBuilderContinuation<T, TProp> : QueryBuilderExpression<T, TProp>
    {
        internal QueryBuilderContinuation(Func<Query<T>, Query<T>> continueWith, Expression<Func<T, TProp>> expression) : base(expression, (q) => new Query<T, TProp>(continueWith(q), expression))
        {
        }

        public QueryBuilderExpression<T, T> Is => new QueryBuilderExpression<T, T>(e => e, (q) => new Query<T,T>(_continueWith(q)));
        public QueryBuilderExpression<T, TProp2> Has<TProp2>(Expression<Func<T, TProp2>> expression)
            => new QueryBuilderExpression<T, TProp2>(expression, (q) => new Query<T, TProp2>(_continueWith(q)));

        public Query<T> Has(Expression<Func<T, bool>> query) => _continueWith(new Query<T>(query));        
    }
}