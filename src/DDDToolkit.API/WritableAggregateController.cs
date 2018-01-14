using DDDToolkit.ApplicationLayer;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDDToolkit.API
{
    public abstract class WritableAggregateController<T, TId> : Controller, IWritableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        private readonly IWritableApplicationService<T, TId> _applicationService;

        protected WritableAggregateController(IWritableApplicationService<T, TId> applicationService)
        {
            _applicationService = applicationService;
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
