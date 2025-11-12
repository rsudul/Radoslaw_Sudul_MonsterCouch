using System;
using System.Collections.Generic;

namespace MonsterCouchTest.Infrastructure
{
    public sealed class ServiceContainer : IServiceResolver, IDisposable
    {
        private readonly Dictionary<Type, Func<IServiceResolver, object>> _factories;
        private readonly Dictionary<Type, object> _singletons;
        private bool _disposed;

        public ServiceContainer(Dictionary<Type, Func<IServiceResolver, object>> factories,
            Dictionary<Type, object> singletons)
        {
            _factories = new Dictionary<Type, Func<IServiceResolver, object>>(factories);
            _singletons = new Dictionary<Type, object>(singletons);
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));

        public object Resolve(Type type)
        {
            if (_singletons.TryGetValue(type, out var single))
            {
                return single;
            }

            if (_factories.TryGetValue(type, out var factory))
            {
                return factory(this);
            }

            throw new InvalidOperationException($"No service registered for type {type}.");
        }

        public bool TryResolve<T>(out T service)
        {
            if (TryResolve(typeof(T), out var obj))
            {
                service = (T)obj;
                return true;
            }
            service = default!;
            return false;
        }

        public bool TryResolve(Type type, out object service)
        {
            if (_singletons.TryGetValue(type, out service))
            {
                return true;
            }

            if (_factories.TryGetValue(type, out var factory))
            {
                service = factory(this);
                return true;
            }

            service = null!;
            return false;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            foreach (var kv in _singletons)
            {
                if (kv.Value is not IDisposable d)
                {
                    continue;
                }

                try
                {
                    d.Dispose();
                }
                catch { }
            }

            _singletons.Clear();
            _factories.Clear();
        }
    }
}
