using DDDToolkit.Core.Interfaces;
using DDDToolkit.ApplicationLayer;

namespace DDDToolkit.API
{
    internal class WritableAggregateControllerImpl<T, TId> : WritableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        internal WritableAggregateControllerImpl(IWritableApplicationService<T, TId> applicationService) : base(applicationService)
        {
        }
    }
}
