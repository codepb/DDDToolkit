using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class QueryBuilderExpression<T, TProp>
    {
        private Expression<Func<T, TProp>> _expression;
        private readonly Func<Query<T>, Query<T>> _continueWith;

        internal QueryBuilderExpression()
        {

        }

        internal QueryBuilderExpression(Expression<Func<T, TProp>> expression, Func<Query<T>, Query<T>> continueWith = null)
        {
            _expression = expression;
            _continueWith = continueWith ?? (q => q);
        }

        private Query<T> CreateQuery(TProp other, Func<Expression, Expression, BinaryExpression> expressionCreator)
        {
            var otherEntity = Expression.Constant(other, typeof(TProp));
            var expression = expressionCreator(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        public Query<T> EqualTo(TProp other)
            => CreateQuery(other, Expression.Equal);


        public Query<T> NotEqualTo(TProp other)
            => CreateQuery(other, Expression.NotEqual);

        public Query<T> GreaterThan(TProp other)
            => CreateQuery(other, Expression.GreaterThan);

        public Query<T> GreaterThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.GreaterThanOrEqual);

        public Query<T> LessThan(TProp other)
            => CreateQuery(other, Expression.LessThan);

        public Query<T> LessThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.LessThanOrEqual);

        public Query<T> Satisfying(IQuery<TProp> query)
            => query.AsExpression().WithParameter(_expression);
 
        public Query<T> Null()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.Equal(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new Query<T>(lambda);
        }

        public Query<T> NotNull()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.NotEqual(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new Query<T>(lambda);
        }
    }
}