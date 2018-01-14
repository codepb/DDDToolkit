using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DDDToolkit.API
{
    public interface IWritableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task<IActionResult> Create([FromBody] T aggregate);
        Task<IActionResult> Delete(TId id);
        Task<IActionResult> Edit(TId id, [FromBody] T aggregate);
    }
}