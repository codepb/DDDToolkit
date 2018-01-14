using DDDToolkit.Core;
using DDDToolkit.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public interface IApplicationService<T, TId>
        : IReadableApplicationService<T, TId>,
        IWritableApplicationService<T, TId>
        where T : class, IAggregateRoot<TId>
    {
    }
}