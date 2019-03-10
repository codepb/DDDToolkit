using System;

namespace DDDToolkit.Core.Interfaces
{
    /// <summary>
    /// <para>
    /// An object that has no conceptual identity. It is
    /// defined by its properties
    /// </para>
    /// <para>
    /// Two value objects are identical if they are the same type
    /// and all of their properties are equal.
    /// </para>
    /// <para>
    /// It is recommended that you make value objects immutable.
    /// As a value object is defined by its properties, you should
    /// not change the values on the value object, but instead
    /// create a new value object with the desired properties
    /// a new object.
    /// </para>
    /// </summary>
    public interface IValueObject : IEquatable<IValueObject>
    {
    }
}
