using System;

namespace DDDToolkit.Core.Interfaces
{
    /// <summary>
    /// <para>
    /// An entity as defined in Domain Driven Design. Entities are objects
    /// that are identified by a thread of continuity and identity.
    /// </para>
    /// <para>
    /// The entity provides an identifier that is used for equality. Any
    /// two entities should be equal if they are of the same type and have
    /// the same value for the identifier
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type used for the Id of the Entity</typeparam>
    public interface IEntity<T> : IEquatable<IEntity<T>>
    {
        /// <summary>
        /// The identifier of the entity. Used for equality.
        /// Two entities of the same type with the same value
        /// for Id should be considered equal.
        /// </summary>
        T Id { get; }
    }
}