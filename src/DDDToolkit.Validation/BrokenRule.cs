using System;

namespace DDDToolkit.Validation
{
    internal class BrokenRule<T, TProp> : IBrokenRule<T, TProp>
    {
        public Func<T, TProp> PropertyAccessor { get; }

        public string Rule { get; }

        public BrokenRule(Func<T, TProp> propertyAccessor, string rule)
        {
            PropertyAccessor = propertyAccessor;
            Rule = rule;
        }
    }
}
