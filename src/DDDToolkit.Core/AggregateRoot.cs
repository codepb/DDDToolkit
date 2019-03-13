using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.Core
{
    /// <summary>
    /// <para>
    /// A semantic difference from <see cref="IEntity{T}"/>, the
    /// aggregate root represents an entity that is resposible for
    /// handling all interactions with the aggregate.
    /// </para>
    /// <para>
    /// Behaviour is identical to <see cref="IEntity{T}"/>, and
    /// equality is defined by the two aggregate roots being of the
    /// same type, with Ids that are equal.
    /// </para>
    /// <para>
    /// In addition to the behaviour of <see cref="IEntity{T}"/>, the
    /// aggregate root should be used by repositories, services, etc to
    /// help control the points of interactions with entities.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of the Id for the Aggregate Root.</typeparam>
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T>
    {    
    }
}
