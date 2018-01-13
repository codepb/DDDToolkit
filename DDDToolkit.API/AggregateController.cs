using DDDToolkit.ApplicationLayer;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.API
{
    public abstract class AggregateController<T, TId> : Controller where T : class, IAggregateRoot<TId>
    {
        private readonly IApplicationService<T, TId> _applicationService;

        protected AggregateController(IApplicationService<T, TId> applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<T> GetById(TId id)
        {
            return await _applicationService.GetById(id);
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await _applicationService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T aggregate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _applicationService.Add(aggregate);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(TId id, [FromBody] T aggregate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _applicationService.Update(id, aggregate);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(TId id)
        {
            await _applicationService.Delete(id);

            return NoContent();
        }
    }
}
