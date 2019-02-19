using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DDDToolkit.Querying
{
    public static class QueryBuilderIEnumerableExtensions
    {
        public static Query<T> Containing<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, T1 other)
        {
            var otherEntity = Expression.Constant(other, typeof(T1));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> NotEmpty<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression)
        {
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 1)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> Empty<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression)
        {
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 1)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> WithAny<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> WithoutAny<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> WithAll<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "All" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> WithNotAll<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "All" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> EqualToSequence<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, IEnumerable<T1> other)
        {
            var otherEntity = Expression.Constant(other, typeof(IEnumerable<T1>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "SequenceEqual" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> NotEqualToSequence<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, IEnumerable<T1> other)
        {
            var otherEntity = Expression.Constant(other, typeof(IEnumerable<T1>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "SequenceEqual" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }
    }
}
