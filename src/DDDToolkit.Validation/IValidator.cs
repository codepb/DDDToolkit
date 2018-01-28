using System;
using System.Linq.Expressions;

namespace DDDToolkit.Validation
{
    public interface IValidator<T>
    {
        bool IsValid(T subject);
        bool IsValidProperty<TProp>(T subject, Expression<Func<T, TProp>> propertyAccessor);
    }
}