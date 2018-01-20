using DDDToolkit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDDToolkit.Core.Querying
{
    public interface IQuery<T, TId> where T : IAggregateRoot<TId>
    {
        IQuery<T, TId> Where(Expression<Func<T, bool>> query);
        IReadOnlyCollection<T> Execute(IQueryable<T> queryable);
    }
}
