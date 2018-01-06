using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DDDToolkit.Core
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        private readonly List<IDomainEvent> _eventsPendingPersistence = new List<IDomainEvent>();
        private readonly Dictionary<Type, MethodInfo> _handleMethods;
        public int Version { get; private set; }

        protected AggregateRoot()
        {
            _handleMethods = GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                .Where(IsEligibleMethod)
                .ToDictionary(m => m.GetParameters()[0].ParameterType);
        }

        public IReadOnlyCollection<IDomainEvent> DequeueAllEvents()
        {
            var events = _eventsPendingPersistence.ToList();

            _eventsPendingPersistence.Clear();
            return events;
        }

        protected void Apply(IDomainEvent @event, bool isNew = true)
        {
            Version++;
            @event.Version = Version;

            CallEventHandler(@event);

            if(isNew)
            {
                _eventsPendingPersistence.Add(@event);
            }
        }
        
        private void CallEventHandler(IDomainEvent @event)
        {
            CallEventHandler(@event, @event.GetType());
        }
        
        private bool CallEventHandler(IDomainEvent @event, Type type)
        {
            if (_handleMethods.ContainsKey(type))
            {
                _handleMethods[type].Invoke(this, new object[] { @event });
                return true;
            }
            else if(type.GetTypeInfo().BaseType != null)
            {
                return CallEventHandler(@event, type.GetTypeInfo().BaseType);
            }
            else
            {
                var interfaces = @event.GetType().GetTypeInfo().ImplementedInterfaces.Where(t => t != type);
                foreach(var implementedInterface in interfaces)
                {
                    if(CallEventHandler(@event, implementedInterface))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private static bool IsEligibleMethod(MethodInfo method)
        {
            return method.Name == "Handle" &&
                   method.GetParameters().Length == 1;
        }
    }
}
