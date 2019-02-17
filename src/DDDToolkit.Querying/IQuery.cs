using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public interface IQuery<T>
    {
        IQuery<T> AndSatisfying(IQuery<T> query);
        IQuery<T> OrSatisfying(IQuery<T> query);
        Expression<Func<T, bool>> AsExpression();
        bool IsSatisfiedBy(T subject);
    }
}
