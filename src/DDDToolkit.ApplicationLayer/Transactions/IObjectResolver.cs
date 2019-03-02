using System;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    public interface IObjectResolver
    {
        bool IsRegistered<T>();
        void Register<T>(object obj) where T : class;
        void RegisterLazy<T>(Lazy<object> lazyObj) where T : class;
        T Resolve<T>() where T : class;
    }
}