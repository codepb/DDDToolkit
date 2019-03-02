using DDDToolkit.Core.Interfaces;
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
            {
                return false;
            }

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

        public static bool operator ==(ValueObject x, ValueObject y)
        {
            if(x is null)
            {
                return y is null;
            }

            return x.Equals(y);
        }

        public static bool operator !=(ValueObject x, ValueObject y)
        {
            return !(x == y);
        }
    }
}
