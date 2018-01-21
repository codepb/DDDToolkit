using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public class QueryBuilder<T> : Query<T>
    {
        internal QueryBuilder(Expression<Func<T, bool>> query) : base(query)
        {
            
        }

        public QueryBuilderContinuation<T> And()
            => new QueryBuilderContinuation<T>((q2) => And(q2));

        public QueryBuilderContinuation<T> Or()
            => new QueryBuilderContinuation<T>((q2) => Or(q2));
    }
}