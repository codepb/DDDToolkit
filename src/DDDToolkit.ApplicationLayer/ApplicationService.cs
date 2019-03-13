using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using FluentQueries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public abstract class ApplicationService
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected Task<ITransaction> BeginTransaction()
        {
            return _unitOfWork.BeginTransaction();
        }
        protected Task<IReadOnlyCollection<T>> Query<T, TId>(IQuery<T> query, QueryOptions queryOptions = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IAggregateRoot<TId>
        {
            return ReadableRepository<T, TId>().Query(query, queryOptions, cancellationToken);
        }

        protected Task<T> FirstOrDefault<T, TId>(IQuery<T> query, string[] onlyIncludePaths = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IAggregateRoot<TId>
        {
            return ReadableRepository<T, TId>().FirstOrDefault(query, onlyIncludePaths, cancellationToken);
        }

        protected Task Add<T, TId>(T entity) where T : class, IAggregateRoot<TId>
        {
            return WritableRepository<T, TId>().Add(entity);
        }

        protected Task Update<T, TId>(T entity) where T : class, IAggregateRoot<TId>
        {
            return WritableRepository<T, TId>().Update(entity);
        }

        protected Task Remove<T, TId>(TId id) where T : class, IAggregateRoot<TId>
        {
            return WritableRepository<T, TId>().Remove(id);
        }

        protected IRepository<T, TId> Repository<T, TId>() where T : class, IAggregateRoot<TId> => _unitOfWork.Repository<T, TId>();

        protected IReadableRepository<T, TId> ReadableRepository<T, TId>() where T : class, IAggregateRoot<TId> => _unitOfWork.ReadableRepository<T, TId>();

        protected IWritableRepository<T, TId> WritableRepository<T, TId>() where T : class, IAggregateRoot<TId> => _unitOfWork.WritableRepository<T, TId>();

        protected Task SaveChanges()
        {
            return _unitOfWork.Save();
        }
    }
}
