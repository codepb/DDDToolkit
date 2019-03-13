using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.Utilities.Extensions
{
    /// <summary>
    /// Extensions to Tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Coverts a Task of a <see cref="List{T}"/> to a Task of a <see cref="IReadOnlyCollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of entity in the List.</typeparam>
        /// <param name="task">The tssk to convert.</param>
        /// <returns>The Task of a List converted to a Task of a IReadOnlyCollection.</returns>
        public static Task<IReadOnlyCollection<T>> ToReadOnlyCollection<T>(this Task<List<T>> task)
            => task.ContinueWith(t => t.Result.AsReadOnly()).ConvertTask().ToTaskOf<IReadOnlyCollection<T>>();

        /// <summary>
        /// Begin converting a Task of a type to a Task of a compatiable type.
        /// </summary>
        /// <typeparam name="T">The current type of the Task.</typeparam>
        /// <param name="task">The Task to convert.</param>
        /// <returns>A <see cref="TaskConverter{T}"/> which enables selecting a Task to convert to.</returns>
        public static TaskConverter<T> ConvertTask<T>(this Task<T> task)
        {
            return new TaskConverter<T>(task);
        }

        /// <summary>
        /// A class that can convert a Task of one type, to a Task of a compatible type.
        /// </summary>
        /// <typeparam name="T">The current type of the Task.</typeparam>
        public class TaskConverter<T>
        {
            private Task<T> _task;

            /// <summary>
            /// Initialises a Task converter that will be able to convert
            /// the supplied Task to a Task of a compatible type.
            /// </summary>
            /// <param name="task">The Task to convert.</param>
            public TaskConverter(Task<T> task)
            {
                _task = task;
            }

            /// <summary>
            /// Creates and returns a new Task of the type specified from the
            /// Task that was supplied to the class.
            /// </summary>
            /// <typeparam name="TResult">The type to convert the Task to.</typeparam>
            /// <returns>A new Task of the type supplied.</returns>
            public Task<TResult> ToTaskOf<TResult>()
                where TResult : class
            {
                if (!typeof(TResult).IsAssignableFrom(typeof(T)))
                {
                    throw new InvalidOperationException($"{typeof(TResult).Name} is not assignable from {typeof(T).Name}");
                }
                return _task.ContinueWith(t => t.Result as TResult);
            }

            /// <summary>
            /// <para>
            /// Creates and returns a new Task of the type specified from the
            /// Task that was supplied to the class.
            /// </para>
            /// <para>
            /// An alias for <see cref="ToTaskOf{TResult}"/>
            /// </para>
            /// </summary>
            /// <typeparam name="TResult">The type to convert the Task to.</typeparam>
            /// <returns>A new Task of the type supplied.</returns>
            public Task<TResult> To<TResult>()
                where TResult : class
                => ToTaskOf<TResult>();
        }

    }
}
