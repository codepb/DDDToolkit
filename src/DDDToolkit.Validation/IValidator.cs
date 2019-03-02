using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public interface IValidator<T>
    {
        bool IsValid(T subject);
        bool PropertyIsValid<TProp>(T subject, Func<T, TProp> propertyAccessor);
        IEnumerable<IBrokenRule<T, object>> GetBrokenRules(T subject);
        IEnumerable<IBrokenRule<T, TProp>> GetBrokenRulesForProperty<TProp>(T subject, Func<T, TProp> propertyAccessor);
    }
}