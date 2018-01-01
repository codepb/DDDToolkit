using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql.Extensions
{
    internal static class TaskExtensions
    {
        public static Task<TResult> AsTaskOf<T, TResult>(this Task<T> task)
        where T : TResult
        where TResult : class
        {
            return task.ContinueWith(t => t.Result as TResult);
        }
    }
}
