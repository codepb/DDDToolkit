using System;

namespace DDDToolkit.Core
{
    public interface IEntity<T> : IEquatable<IEntity<T>>
    {
        T Id { get; }
    }
}