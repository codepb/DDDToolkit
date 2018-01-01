using System;
using System.Collections.Generic;
using System.Reflection;

namespace DDDToolkit.Core
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            T other = obj as T;

            return Equals(other);
        }

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

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType)
                return false;

            PropertyInfo[] properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                object value1 = property.GetValue(other);
                object value2 = property.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            Type t = GetType();

            List<PropertyInfo> properties = new List<PropertyInfo>();

            while (t != typeof(object))
            {
                properties.AddRange(t.GetProperties(BindingFlags.Instance | BindingFlags.Public));

                t = t.GetTypeInfo().BaseType;
            }

            return properties;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}
