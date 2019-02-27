using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer
{
    public class WritableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IWritableRepository<T, TId> _repository;

        public WritableApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.WritableRepository<T, TId>();
        }
    }
}
