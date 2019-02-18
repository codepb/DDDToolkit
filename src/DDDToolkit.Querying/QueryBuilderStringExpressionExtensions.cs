﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DDDToolkit.Querying
{
    public static class QueryBuilderStringExpressionExtensions
    {
        private static Query<T> BuildQuery<T>(string methodName, QueryBuilderExpression<T, string> queryBuilderExpression, string other)
        {
            var otherEntity = Expression.Constant(other, typeof(string));
            MethodInfo method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            var expression = Expression.Call(queryBuilderExpression._expression.Body, method, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> Contains<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("Contains", queryBuilderExpression, other);

        public static Query<T> StartsWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("StartsWith", queryBuilderExpression, other);

        public static Query<T> EndsWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("EndsWith", queryBuilderExpression, other);

        public static Query<T> NullOrWhiteSpace<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("IsNullOrWhiteSpace", queryBuilderExpression, other);

        public static Query<T> NullOrEmpty<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("IsNullOrEmpty", queryBuilderExpression, other);
    }
}