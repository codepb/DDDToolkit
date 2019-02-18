using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class Query<T> : IQuery<T> 
    {
        private Expression<Func<T, bool>> _expression;

        public Query() { }

        public Query(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public Query(IQuery<T> query) : this(query.AsExpression())
        {
            
        }

        public QueryBuilderContinuation<T> And
            => new QueryBuilderContinuation<T>((q2) => CreateNewQuery(q2, Expression.And));

        public QueryBuilderContinuation<T> Or
            => new QueryBuilderContinuation<T>((q2) => CreateNewQuery(q2, Expression.Or));

        protected void Define(IQuery<T> query)
        {
            if(_expression != null)
            {
                throw new InvalidOperationException("Query already defined.");
            }
            _expression = query.AsExpression();
        }

        protected virtual Query<T> CreateNewQuery(IQuery<T> query, Func<Expression, Expression, Expression> combiner)
        {
            var e2 = query.AsExpression();
            var newE2 = new ParameterVisitor(e2.Parameters, _expression.Parameters).VisitAndConvert(e2.Body, nameof(CreateNewQuery));
            var e3 = combiner(_expression.Body, newE2);
            
            var lambda = Expression.Lambda<Func<T, bool>>(e3, _expression.Parameters);
            return new Query<T>(lambda);
        }

        public IQuery<T> AndSatisfying(IQuery<T> query) => CreateNewQuery(query, Expression.AndAlso);
        public IQuery<T> OrSatisfying(IQuery<T> query) => CreateNewQuery(query, Expression.OrElse);        

        public Expression<Func<T, bool>> AsExpression() => _expression;
    
        public bool IsSatisfiedBy(T subject) => AsExpression().Compile()(subject);

        public static Query<T> Satisfies(Expression<Func<T, bool>> query) => new Query<T>(query);

        public static QueryBuilderExpression<T, TProp> Has<TProp>(Expression<Func<T, TProp>> expression)
            => new QueryBuilderExpression<T, TProp>(expression);

        public static QueryBuilderExpression<T, T> Is => Has(e => e);

        public static implicit operator Query<T>(Expression<Func<T, bool>> query) => new Query<T>(query);
    }
}