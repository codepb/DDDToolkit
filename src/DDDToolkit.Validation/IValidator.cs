using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public interface IValidator<T>
    {
        bool IsValid(T subject);
        bool IsValidProperty<TProp>(T subject, Func<T, TProp> propertyAccessor);
        IEnumerable<IBrokenRule<T, object>> Validate(T subject);
        IEnumerable<IBrokenRule<T, TProp>> ValidateProperty<TProp>(T subject, Func<T, TProp> propertyAccessor);
    }
}