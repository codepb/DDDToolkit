using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer
{
    public class ApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T, TId> _repository;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Repository<T, TId>();
        }        
    }
}
