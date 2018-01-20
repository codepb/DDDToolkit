using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class Query<T> : IQuery<T> 
    {
        private readonly Expression<Func<T, bool>> _expression = null;

        public Query(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        private Query<T> CreateNewQuery(Expression<Func<T, bool>> e2, Func<Expression, Expression, Expression> combiner)
        {
            var newE2 = new ParameterVisitor(e2.Parameters, _expression.Parameters)
              .VisitAndConvert(e2.Body, nameof(CreateNewQuery));
            var e3 = combiner(_expression.Body, newE2);
            
            var lambda = Expression.Lambda<Func<T, bool>>(e3, _expression.Parameters);
            return new Query<T>(lambda);
        }

        public IQuery<T> And(Expression<Func<T, bool>> query)
            => CreateNewQuery(query, Expression.AndAlso);
        public IQuery<T> And(IQuery<T> query) => And(query.AsExpression());

        public IQuery<T> Or(Expression<Func<T, bool>> query)
            => CreateNewQuery(query, Expression.OrElse);
        public IQuery<T> Or(IQuery<T> query) => Or(query.AsExpression());

        public Expression<Func<T, bool>> AsExpression()
            => _expression;

        public static Query<T> Where(Expression<Func<T, bool>> query)
            => new Query<T>(query);
    }

    class ParameterVisitor : ExpressionVisitor
    {
        private readonly IReadOnlyList<ParameterExpression> _from, _to;
        public ParameterVisitor(
            IReadOnlyList<ParameterExpression> from,
            IReadOnlyList<ParameterExpression> to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");
            if (from.Count != to.Count) throw new InvalidOperationException(
                  "Parameter lengths must match");
            _from = from;
            _to = to;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            for (int i = 0; i < _from.Count; i++)
            {
                if (node == _from[i]) return _to[i];
            }
            return node;
        }
    }
}