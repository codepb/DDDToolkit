using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core.Interfaces;
using FluentQueries;

namespace DDDToolkit.ApplicationLayer
{
    public interface IReadableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> GetPaged(PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetById(TId id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> GetWhere(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> GetWhere(IQuery<T> query, PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken));
    }
}