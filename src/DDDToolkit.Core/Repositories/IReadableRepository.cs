using DDDToolkit.Core.Interfaces;
using DDDToolkit.Core.Querying;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDToolkit.Core.Repositories
{
    public interface IReadableRepository<T, TId>
        where T : IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query);
        Task<IReadOnlyCollection<T>> Query(IAsyncQuery<T, TId> query);
        IAsyncQuery<T, TId> Where(Expression<Func<T, bool>> query);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> query);
        Task<T> GetById(TId id);
    }
}
