using System.Collections.Generic;

namespace DDDToolkit.Core
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as IEntity<T>);
        }

        public bool Equals(IEntity<T> other)
        {
            return other != null && other.GetType() == GetType() &&
                   EqualityComparer<T>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<T>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Entity<T> entity1, Entity<T> entity2)
        {
            return EqualityComparer<Entity<T>>.Default.Equals(entity1, entity2);
        }

        public static bool operator !=(Entity<T> entity1, Entity<T> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
