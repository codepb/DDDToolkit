using DDDToolkit.Core;
using DDDToolkit.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDDToolkit.EntityFramework.Extensions
{
    /// <summary>
    /// Extensions to include properties in Entity Framework.
    /// </summary>
    public static class IncludeExtensions
    {
        /// <summary>
        /// <para>
        /// Include every navigation property on the entity.
        /// Navigate through every property that can be included, includes it,
        /// then recursively repeats the operation for all children until the
        /// whole property tree is included.
        /// </para>
        /// <para>
        /// Note: do not use this if there are any cyclical references. Due to
        /// the recursive nature, this will cause a stack overflow.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="source">The Queryable to operate on.</param>
        /// <returns>A new Queryable with all child properties included in the Entity Framework query.</returns>
        /// <exception cref="StackOverflowException">Thrown when there is a cyclic reference on the
        /// entity, or any child entity.</exception>
        public static IQueryable<T> IncludeEverything<T>(this IQueryable<T> source) where T : class
        {
            var type = source.GetType();
            var ret = source;
            foreach (var property in GetPropertyPaths(typeof(T)))
            {
                ret = ret.Include(property);
            }

            return ret;
        }

        private static IEnumerable<string> GetPropertyPaths(Type type, Type prevType = null, string rootPath = "")
        {
            var entityProperties = GetEntityProperties(type, prevType);
            var valueProperties = GetValueProperties(type);

            if (entityProperties.Count == 0 && valueProperties.Count == 0)
            {
                return string.IsNullOrEmpty(rootPath) ? new string[0] : new[] { rootPath };
            }

            return entityProperties
                .SelectMany(p => GetPropertyPaths(GetPropertyType(p.PropertyType), type, CombinePaths(rootPath, p.Name)))
                .Concat(valueProperties.Select(p => CombinePaths(rootPath, p.Name)));
        }

        private static IReadOnlyCollection<PropertyInfo> GetEntityProperties(Type type, Type prevType = null) =>
            type
            .GetProperties()
            .Where(p => IsEntityType(p.PropertyType, prevType))
            .ToList();

        private static IReadOnlyCollection<PropertyInfo> GetValueProperties(Type type) =>
            type
            .GetProperties()
            .Where(p => IsValueType(p.PropertyType))
            .ToList();

        private static bool IsValueType(Type type) =>
            type.BaseType != null
            && type.BaseType.IsGenericType
            && type.BaseType.GetGenericTypeDefinition() == typeof(IValueObject);
            

        private static Type GetPropertyType(Type type)
        {
            var collectionInterface = type.GetInterfaces().FirstOrDefault(t => IsEntityCollectionInterface(t, null));

            return collectionInterface != null ? collectionInterface.GetGenericArguments()[0] : type;
        }

        private static bool IsEntityCollectionInterface(Type type, Type ignoreType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>) && IsEntityType(type.GetGenericArguments()[0], ignoreType);
        }

        private static bool IsEntityCollection(Type type, Type ignoreType)
        {
            return type.GetInterfaces().Any(t => IsEntityCollectionInterface(t, ignoreType));
        }

        private static string CombinePaths(string rootPath, string propertyName)
        {
            if (string.IsNullOrEmpty(rootPath))
            {
                return propertyName;
            }
            else
            {
                return $"{rootPath}.{propertyName}";
            }
        }

        private static bool DevivesFromEntity(Type type, Type ignoreType)
        {
            if (type == ignoreType)
            {
                return false;
            }

            if (type.BaseType != null)
            {
                if (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(Entity<>))
                {
                    return true;
                }
                return DevivesFromEntity(type.BaseType, ignoreType);
            }

            return false;
        }

        private static bool IsEntityType(Type type, Type ignoreType)
        {
            if (DevivesFromEntity(type, ignoreType))
            {
                return true;
            }

            if (IsEntityCollection(type, ignoreType))
            {
                return true;
            }

            return false;
        }
    }
}
