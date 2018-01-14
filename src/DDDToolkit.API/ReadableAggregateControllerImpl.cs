using DDDToolkit.ApplicationLayer;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.API
{
    internal class ReadableAggregateControllerImpl<T, TId> : ReadableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        internal ReadableAggregateControllerImpl(IReadableApplicationService<T, TId> applicationService) : base(applicationService)
        {
        }
    }
}
