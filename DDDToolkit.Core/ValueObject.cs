using System;
using System.Collections.Generic;
using System.Reflection;

namespace DDDToolkit.Core
{
    public abstract class ValueObject : IValueObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as IValueObject;

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

        public virtual bool Equals(IValueObject other)
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

        public static bool operator ==(ValueObject x, ValueObject y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ValueObject x, ValueObject y)
        {
            return !(x == y);
        }
    }
}
