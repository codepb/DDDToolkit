using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core.Repositories
{
    public interface IRepository<T, TId> 
        : IReadableRepository<T, TId>
        , IWritableRepository<T, TId>
        where T : AggregateRoot<TId>
    {
    }
}
