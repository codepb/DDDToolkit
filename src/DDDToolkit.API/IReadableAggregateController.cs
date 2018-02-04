using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DDDToolkit.API
{
    public interface IReadableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(TId id);
    }
}