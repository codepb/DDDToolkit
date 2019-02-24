using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using FluentQueries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public class ApplicationService<T, TId> : IApplicationService<T, TId>
        where T : class, IAggregateRoot<TId>
    {
        private IReadableApplicationService<T, TId> _readableApplicationService;
        private IWritableApplicationService<T, TId> _writableApplicationService;

        protected readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readableApplicationService = new ReadableApplicationService<T, TId>(unitOfWork);
            _writableApplicationService = new WritableApplicationService<T, TId>(unitOfWork);
        }

        public virtual Task<IReadOnlyCollection<T>> Query(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken)) => _readableApplicationService.GetWhere(query, cancellationToken);

        public virtual Task<T> GetById(TId id, CancellationToken cancellationToken = default(CancellationToken)) => _readableApplicationService.GetById(id, cancellationToken);

        public virtual Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken = default(CancellationToken)) => _readableApplicationService.GetAll(cancellationToken);

        public Task<IReadOnlyCollection<T>> GetPaged(PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken))
            => _readableApplicationService.GetPaged(pagingOptions, cancellationToken);

        public Task<IReadOnlyCollection<T>> GetWhere(IQuery<T> query, CancellationToken cancellationToken = default(CancellationToken))
            => _readableApplicationService.GetWhere(query, cancellationToken);

        public Task<IReadOnlyCollection<T>> GetWhere(IQuery<T> query, PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken))
            => _readableApplicationService.GetWhere(query, pagingOptions, cancellationToken);

        public virtual Task Add(T aggregate) => _writableApplicationService.Add(aggregate);

        public virtual Task Update(TId id, T aggregate) => _writableApplicationService.Update(id, aggregate);

        public virtual Task Delete(TId id) => _writableApplicationService.Delete(id);

        
    }
}
