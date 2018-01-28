using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Querying
{
    public static class ObjectExtensions
    {
        public static bool Satisfies<T>(this T obj, IQuery<T> query) => query.IsSatisfiedBy(obj);
    }
}
