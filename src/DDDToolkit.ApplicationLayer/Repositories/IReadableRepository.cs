using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core.Interfaces;
using FluentQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer.Repositories
{
    public interface IReadableRepository<T, TId>
        where T : IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> Query(QueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query, QueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<IReadOnlyCollection<T>> Query(IQuery<T> query, QueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> FirstOrDefault(Expression<Func<T, bool>> query, string[] onlyIncludePaths = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> FirstOrDefault(IQuery<T> query, string[] onlyIncludePaths = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetById(TId id, string[] onlyIncludePaths = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
