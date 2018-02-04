﻿using DDDToolkit.ApplicationLayer;
using DDDToolkit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDDToolkit.API
{
    public abstract class ReadableAggregateController<T, TId> : Controller, IReadableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        private readonly IReadableApplicationService<T, TId> _applicationService;

        protected ReadableAggregateController(IReadableApplicationService<T, TId> applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(TId id)
        {
            return Ok(await _applicationService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _applicationService.GetAll());
        }
    }
}
