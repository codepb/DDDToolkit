using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDToolkit.Core.Repositories
{
    public interface IReadableRepository<T, TId> where T : AggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query);
        Task<T> GetById(TId id);
    }
}
