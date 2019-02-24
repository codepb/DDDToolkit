using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using FluentQueries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public class ReadableApplicationService<T, TId> : IReadableApplicationService<T, TId>
        where T : class, IAggregateRoot<TId>
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ReadableApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual Task<T> GetById(TId id, CancellationToken cancellationToken = default(CancellationToken))
            => _unitOfWork.ReadableRepository<T, TId>().GetById(id, cancellationToken);

        public virtual Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
            => _unitOfWork.ReadableRepository<T, TId>().Query(cancellationToken);

        public virtual Task<IReadOnlyCollection<T>> GetPaged(PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken))
            => _unitOfWork.ReadableRepository<T, TId>().Query(pagingOptions, cancellationToken);

        public virtual Task<IReadOnlyCollection<T>> GetWhere(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken))
            => _unitOfWork.ReadableRepository<T, TId>().Query(query, cancellationToken);

        public virtual Task<IReadOnlyCollection<T>> GetWhere(IQuery<T> query, PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken))
            => _unitOfWork.ReadableRepository<T, TId>().Query(query, pagingOptions, cancellationToken);
    }
}
