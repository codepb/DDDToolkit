using DDDToolkit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DDDToolkit.Core
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
    public abstract class ValueObject : IValueObject
    {
        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two value objects are considered equal if they are the same type
        /// and have all properties equal.
        /// </para>
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as IValueObject;

            return Equals(other);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>The hash of the entity.</returns>
        public override int GetHashCode()
        {
            IEnumerable<PropertyInfo> properties = GetProperties();

            int startValue = 17;
            int multiplier = 59;

            int hashCode = startValue;

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two value objects are considered equal if they are the same type
        /// and have all properties equal.
        /// </para>
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public virtual bool Equals(IValueObject other)
        {
            if (other == null)
            {
                return false;
            }

            var t = GetType();
            var otherType = other.GetType();

            if (t != otherType)
            {
                return false;
            }

            var properties = GetProperties();

            foreach (var property in properties)
            {
                var value1 = property.GetValue(other);
                var value2 = property.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            Type t = GetType();

            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two value objects are considered equal if they are the same type
        /// and have all properties equal.
        /// </para>
        /// </summary>
        /// <param name="x">The first value object to compare.</param>
        /// <param name="y">The second value object to compare.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public static bool operator ==(ValueObject x, ValueObject y)
        {
            if(x is null)
            {
                return y is null;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// <para>
        /// Determines whether the specified object is not equal to
        /// the current object.
        /// </para>
        /// <para>
        /// Two value objects are considered not equal if they are different types
        /// or do not have all properties equal.
        /// </para>
        /// </summary>
        /// <param name="x">The first value object to compare.</param>
        /// <param name="y">The second value object to compare.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public static bool operator !=(ValueObject x, ValueObject y)
        {
            return !(x == y);
        }
    }
}
