using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DDDToolkit.Validation
{
    public interface IBrokenRule<in T, out TProp>
    {
        Func<T, TProp> PropertyAccessor { get; }
        string Rule { get; }
    }
}
