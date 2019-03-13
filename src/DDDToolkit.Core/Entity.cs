using DDDToolkit.Core.Interfaces;
using System.Collections.Generic;

namespace DDDToolkit.Core
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
    public abstract class Entity<T> : IEntity<T>
    {
        /// <summary>
        /// The identifier of the entity. Used for equality.
        /// Two entities of the same type with the same value
        /// for Id should be considered equal.
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two entities are considered equal if they are the same type
        /// and have Ids that are equal.
        /// </para>
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IEntity<T>);
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two entities are considered equal if they are the same type
        /// and have Ids that are equal.
        /// </para>
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public bool Equals(IEntity<T> other)
        {
            return other != null && other.GetType() == GetType() &&
                   EqualityComparer<T>.Default.Equals(Id, other.Id);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>The hash of the entity.</returns>
        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<T>.Default.GetHashCode(Id);
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two entities are considered equal if they are the same type
        /// and have Ids that are equal.
        /// </para>
        /// </summary>
        /// <param name="entity1">The first entity to compare.</param>
        /// <param name="entity2">The second entity to compare.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public static bool operator ==(Entity<T> entity1, Entity<T> entity2)
        {
            return EqualityComparer<Entity<T>>.Default.Equals(entity1, entity2);
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is not equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two entities are considered not equal if they are different types
        /// or have Ids that are not equal.
        /// </para>
        /// </summary>
        /// <param name="entity1">The first entity to compare.</param>
        /// <param name="entity2">The second entity to compare.</param>
        /// <returns>True if the objects are not equal, false otherwise.</returns>
        public static bool operator !=(Entity<T> entity1, Entity<T> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
