using DDDToolkit.ApplicationLayer.Transactions;
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

        protected Task SaveChanges()
        {
            return _unitOfWork.Save();
        }
    }
}
