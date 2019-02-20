using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DDDToolkit.Querying
{
    public static class QueryBuilderBooleanExtensions
    {
        private static Query<T> BuildQuery<T>(ConstantExpression otherEntity, QueryBuilderExpression<T, bool> queryBuilderExpression)
        {
            var expression = Expression.Equal(queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> True<T>(this QueryBuilderExpression<T, bool> queryBuilderExpression)
            => BuildQuery(Expression.Constant(true, typeof(bool)), queryBuilderExpression);

        public static Query<T> False<T>(this QueryBuilderExpression<T, bool> queryBuilderExpression)
            => BuildQuery(Expression.Constant(false, typeof(bool)), queryBuilderExpression);
    }
}
