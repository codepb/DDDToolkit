using System;

namespace DDDToolkit.Validation
{
    public interface IBrokenRule<in T, out TProp>
    {
        Func<T, TProp> PropertyAccessor { get; }
        string Rule { get; }
    }
}
