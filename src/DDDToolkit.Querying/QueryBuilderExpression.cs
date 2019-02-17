using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DDDToolkit.Querying
{
    public class QueryBuilderExpression<T, TProp>
    {
        private Expression<Func<T, TProp>> _expression;
        protected readonly Func<Query<T>, Query<T, TProp>> _continueWith;

        internal QueryBuilderExpression(Expression<Func<T, TProp>> expression, Func<Query<T>, Query<T, TProp>> continueWith = null)
        {
            _expression = expression;
            _continueWith = continueWith ?? (q => new Query<T, TProp>(q, _expression));
        }

        private Query<T, TProp> CreateQuery(TProp other, Func<Expression, Expression, BinaryExpression> expressionCreator)
        {
            var otherEntity = Expression.Constant(other, typeof(TProp));
            var expression = expressionCreator(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T, TProp>(lambda, _expression));
        }

        public Query<T, TProp> EqualTo(TProp other)
            => CreateQuery(other, Expression.Equal);


        public Query<T, TProp> NotEqualTo(TProp other)
            => CreateQuery(other, Expression.NotEqual);

        public Query<T, TProp> EqualToAnyOf(IEnumerable<TProp> values)
        {
            var method = typeof(Enumerable).GetMethods().Where(m => m.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(TProp));
            var otherEntity = Expression.Constant(values, typeof(IEnumerable<TProp>));
            var expression = Expression.Call(method, otherEntity, _expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        public Query<T, TProp> NotEqualToAnyOf(IEnumerable<TProp> values)
        {
            var method = typeof(Enumerable).GetMethods().Where(m => m.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(TProp));
            var otherEntity = Expression.Constant(values, typeof(IEnumerable<TProp>));
            var expression = Expression.Call(method, otherEntity, _expression.Body);
            var notExpression = Expression.Not(expression);
            var lambda = Expression.Lambda<Func<T, bool>>(notExpression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        public Query<T, TProp> EqualToAnyOf(params TProp[] values) =>
            EqualToAnyOf((IEnumerable<TProp>)values);
 
        public Query<T, TProp> GreaterThan(TProp other)
            => CreateQuery(other, Expression.GreaterThan);

        public Query<T, TProp> GreaterThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.GreaterThanOrEqual);

        public Query<T, TProp> LessThan(TProp other)
            => CreateQuery(other, Expression.LessThan);

        public Query<T, TProp> LessThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.LessThanOrEqual);

        public Query<T> Satisfying(IQuery<TProp> query)
            => Satisfying(query.AsExpression());

        public Query<T> Satisfying(Expression<Func<TProp, bool>> query)
            => query.WithParameter(_expression);

        public Query<T, TProp> Null()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.Equal(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new Query<T, TProp>(lambda, _expression);
        }

        public Query<T, TProp> NotNull()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.NotEqual(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return new Query<T, TProp>(lambda, _expression);
        }
    }
}