using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class QueryBuilderExpression<T, TProp>
    {
        private Expression<Func<T, TProp>> _expression;
        private readonly Func<IQuery<T>, IQuery<T>> _continueWith;

        internal QueryBuilderExpression()
        {

        }

        internal QueryBuilderExpression(Expression<Func<T, TProp>> expression, Func<IQuery<T>, IQuery<T>> continueWith = null)
        {
            _expression = expression;
            _continueWith = continueWith ?? (q => q);
        }

        private QueryBuilder<T> CreateQuery(TProp other, Func<Expression, Expression, BinaryExpression> expressionCreator)
        {
            var otherEntity = Expression.Constant(other, typeof(TProp));
            var expression = expressionCreator(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new QueryBuilder<T>(lambda);
        }

        public QueryBuilder<T> EqualTo(TProp other)
            => CreateQuery(other, Expression.Equal);


        public QueryBuilder<T> NotEqualTo(TProp other)
            => CreateQuery(other, Expression.NotEqual);

        public QueryBuilder<T> GreaterThan(TProp other)
            => CreateQuery(other, Expression.GreaterThan);

        public QueryBuilder<T> GreaterThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.GreaterThanOrEqual);

        public QueryBuilder<T> LessThan(TProp other)
            => CreateQuery(other, Expression.LessThan);

        public QueryBuilder<T> LessThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.LessThanOrEqual);

        public QueryBuilder<T> Null()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.Equal(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new QueryBuilder<T>(lambda);
        }

        public QueryBuilder<T> NotNull()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.NotEqual(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new QueryBuilder<T>(lambda);
        }
    }
}