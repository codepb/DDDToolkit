using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class Query<T> : IQuery<T> 
    {
        private readonly Expression<Func<T, bool>> _expression;

        public Query(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        private Query<T> CreateNewQuery(IQuery<T> query, Func<Expression, Expression, Expression> combiner)
        {
            var e2 = query.AsExpression();
            var newE2 = new ParameterVisitor(e2.Parameters, _expression.Parameters).VisitAndConvert(e2.Body, nameof(CreateNewQuery));
            var e3 = combiner(_expression.Body, newE2);
            
            var lambda = Expression.Lambda<Func<T, bool>>(e3, _expression.Parameters);
            return new Query<T>(lambda);
        }

        public IQuery<T> And(IQuery<T> query) => CreateNewQuery(query, Expression.AndAlso);
        public IQuery<T> Or(IQuery<T> query) => CreateNewQuery(query, Expression.OrElse);

        public Expression<Func<T, bool>> AsExpression() => _expression;

        public bool EvaluateOn(T subject) => AsExpression().Compile()(subject);

        public static Query<T> Has(Expression<Func<T, bool>> query) => new Query<T>(query);

        public static QueryBuilderExpression<T, TProp> Has<TProp>(Expression<Func<T, TProp>> expression)
            => new QueryBuilderExpression<T, TProp>(expression);

        public static QueryBuilderExpression<T, T> Is => Has(e => e);
    }
}