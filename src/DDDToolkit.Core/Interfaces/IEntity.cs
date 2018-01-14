using System;

namespace DDDToolkit.Core.Interfaces
{
    public interface IEntity<T> : IEquatable<IEntity<T>>
    {
        T Id { get; }
    }
}