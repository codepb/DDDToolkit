using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using FluentQueries;
using System.Collections.Generic;
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

        public virtual Task<T> GetById(TId id)
            => _unitOfWork.ReadableRepository<T, TId>().GetById(id);

        public virtual Task<IReadOnlyCollection<T>> GetAll()
            => _unitOfWork.ReadableRepository<T, TId>().GetAll();

        public virtual Task<IReadOnlyCollection<T>> Query(IQuery<T> query)
            => _unitOfWork.ReadableRepository<T, TId>().Query(query);
    }
}
