using DDDToolkit.Core.Interfaces;
using FluentQueries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.Core.Repositories
{
    public interface IReadableRepository<T, TId>
        where T : IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> Query(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> QueryWithChildren(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> QueryWithChildren(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> FirstOrDefault(Expression<Func<T, bool>> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> FirstOrDefault(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetById(TId id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
