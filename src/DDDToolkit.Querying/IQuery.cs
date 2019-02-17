using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public interface IQuery<T>
    {
        IQuery<T> AndSatisfies(IQuery<T> query);
        IQuery<T> OrSatisfies(IQuery<T> query);
        Expression<Func<T, bool>> AsExpression();
        bool IsSatisfiedBy(T subject);
    }
}
