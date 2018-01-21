using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public interface IQuery<T>
    {
        IQuery<T> And(Expression<Func<T, bool>> query);
        IQuery<T> And(IQuery<T> query);
        IQuery<T> Or(Expression<Func<T, bool>> query);
        IQuery<T> Or(IQuery<T> query);
        Expression<Func<T, bool>> AsExpression();
    }
}
