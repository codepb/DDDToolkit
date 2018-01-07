using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDDToolkit.Core
{
    class EventHandler : IEventHandler
    {
        private readonly Dictionary<Type, MethodInfo> _handleMethods;
        private readonly object _parent;

        public EventHandler(object parent)
        {
            _handleMethods = parent.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                .Where(IsEligibleMethod)
                .ToDictionary(m => m.GetParameters()[0].ParameterType);
            _parent = parent;
        }

        public void Handle(IDomainEvent @event)
        {
            CallEventHandler(@event, @event.GetType());
        }

        private bool CallEventHandler(IDomainEvent @event, Type type)
        {
            if (TryCallHandlerForType(type, @event))
            {
                return true;
            }
            else if (type.GetTypeInfo().BaseType != null)
            {
                return CallEventHandler(@event, type.GetTypeInfo().BaseType);
            }
            else
            {
                return TryCallHandlerForInterfaces(type, @event);
            }
        }

        private bool TryCallHandlerForType(Type type, IDomainEvent @event)
        {
            if (_handleMethods.ContainsKey(type))
            {
                _handleMethods[type].Invoke(_parent, new object[] { @event });
                return true;
            }
            return false;
        }

        private bool TryCallHandlerForInterfaces(Type type, IDomainEvent @event)
        {
            var interfaces = @event.GetType().GetTypeInfo().ImplementedInterfaces.Where(t => t != type);
            foreach (var implementedInterface in interfaces)
            {
                if (TryCallHandlerForType(implementedInterface, @event))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsEligibleMethod(MethodInfo method)
        {
            return method.Name == "Handle" &&
                   method.GetParameters().Length == 1;
        }
    }
}
