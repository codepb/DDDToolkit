using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DDDToolkit.API
{
    public interface IAggregateController<T, TId> 
        : IReadableAggregateController<T, TId>
        , IWritableAggregateController<T, TId>
        where T : class, IAggregateRoot<TId>
    {
    }
}