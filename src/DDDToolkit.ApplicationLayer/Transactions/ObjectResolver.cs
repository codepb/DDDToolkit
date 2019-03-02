using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    class ObjectResolver : IObjectResolver
    {
        private Dictionary<Type, Lazy<object>> _objects = new Dictionary<Type, Lazy<object>>();

        public bool IsRegistered<T>()
        {
            return _objects.ContainsKey(typeof(T));
        }

        public void Register<T>(object obj) where T : class
        {
            RegisterLazy<T>(new Lazy<object>(() => obj));
        }

        public void RegisterLazy<T>(Lazy<object> lazyObj) where T : class
        {
            if (IsRegistered<T>())
            {
                _objects[typeof(T)] = lazyObj;
            }
            else
            {
                _objects.Add(typeof(T), lazyObj);
            }
        }

        public virtual T Resolve<T>() where T : class
        {
            if (!IsRegistered<T>())
            {
                throw new InvalidOperationException($"No object of Type {typeof(T)} registered");
            }
            return _objects[typeof(T)].Value as T;
        }
    }
}
