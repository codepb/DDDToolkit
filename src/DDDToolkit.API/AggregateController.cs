﻿using DDDToolkit.ApplicationLayer;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.API
{
    public abstract class AggregateController<T, TId> : Controller, IAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        private readonly IReadableAggregateController<T, TId> _readableAggregateController;
        private readonly IWritableAggregateController<T, TId> _writableAggregateController;

        protected AggregateController(IApplicationService<T, TId> applicationService)
        {
            _readableAggregateController = new ReadableAggregateControllerImpl<T, TId>(applicationService);
            _writableAggregateController = new WritableAggregateControllerImpl<T, TId>(applicationService);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<T> GetById(TId id) => _readableAggregateController.GetById(id);

        [HttpGet]
        public Task<IReadOnlyCollection<T>> GetAll() => _readableAggregateController.GetAll();

        [HttpPost]
        public Task<IActionResult> Create([FromBody] T aggregate) => _writableAggregateController.Create(aggregate);

        [HttpPut]
        [Route("{id}")]
        public Task<IActionResult> Edit(TId id, [FromBody] T aggregate) => _writableAggregateController.Edit(id, aggregate);

        [HttpDelete]
        [Route("{id}")]
        public Task<IActionResult> Delete(TId id) => _writableAggregateController.Delete(id);
    }
}
