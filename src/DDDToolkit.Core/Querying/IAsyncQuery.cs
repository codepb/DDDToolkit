using DDDToolkit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDToolkit.Core.Querying
{
    public interface IAsyncQuery<T, TId> where T : IAggregateRoot<TId>
    {
        IAsyncQuery<T, TId> Where(Expression<Func<T, bool>> query);
        Task<IReadOnlyCollection<T>> Execute(IQueryable<T> queryable);
    }
}
