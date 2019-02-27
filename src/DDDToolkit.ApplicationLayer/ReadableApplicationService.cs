using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer
{
    public class ReadableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IReadableRepository<T, TId> _repository;

        public ReadableApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.ReadableRepository<T, TId>();
        }
    }
}
