﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.Utilities.Extensions
{
    public static class TaskExtensions
    {
        public static TaskConverter<T> ConvertTask<T>(this Task<T> task)
        {
            return new TaskConverter<T>(task);
        }

        public class TaskConverter<T>
        {
            private Task<T> _task;

            public TaskConverter(Task<T> task)
            {
                _task = task;
            }

            public Task<TResult> To<TResult>()
                where TResult : class
            {
                if (!typeof(TResult).IsAssignableFrom(typeof(T)))
                {
                    throw new InvalidOperationException($"{typeof(TResult).Name} is not assignable from {typeof(T).Name}");
                }
                return _task.ContinueWith(t => t.Result as TResult);
            }
        }

    }
}