using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DDDToolkit.Querying
{
    public class QueryBuilderExpression<T, TProp>
    {
        internal readonly Expression<Func<T, TProp>> _expression;
        internal readonly Func<Query<T>, Query<T>> _continueWith;

        internal QueryBuilderExpression(Expression<Func<T, TProp>> expression, Func<Query<T>, Query<T>> continueWith = null)
        {
            _expression = expression;
            _continueWith = continueWith ?? (q => new Query<T>(q));
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

        public Query<T> EqualToAnyOf(IEnumerable<TProp> values)
        {
            var method = typeof(Enumerable).GetMethods().Where(m => m.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(TProp));
            var otherEntity = Expression.Constant(values, typeof(IEnumerable<TProp>));
            var expression = Expression.Call(method, otherEntity, _expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        public Query<T> NotEqualToAnyOf(IEnumerable<TProp> values)
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

        public Query<T> EqualToAnyOf(params TProp[] values) =>
            EqualToAnyOf((IEnumerable<TProp>)values);
 
        public Query<T> GreaterThan(TProp other)
            => CreateQuery(other, Expression.GreaterThan);

        public Query<T> GreaterThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.GreaterThanOrEqual);

        public Query<T> LessThan(TProp other)
            => CreateQuery(other, Expression.LessThan);

        public Query<T> LessThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.LessThanOrEqual);

        public Query<T> Satisfying(IQuery<TProp> query)
            => Satisfying(query.AsExpression());

        public Query<T> Satisfying(Expression<Func<TProp, bool>> query)
            => _continueWith(new Query<T>(query.WithParameter(_expression)));

        public Query<T> Null()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.Equal(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        public Query<T> NotNull()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.NotEqual(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }
    }
}