using System;
using System.Collections.Generic;

namespace MonsterCouchTest.Infrastructure
{
    public sealed class ServiceRegistry
    {
        private readonly Dictionary<Type, Func<IServiceResolver, object>> _factories = new();
        private readonly Dictionary<Type, object> _singletons = new();

        public ServiceRegistry RegisterSingleton<TService>(TService instance)
        {
            _singletons[typeof(TService)] = instance!;
            return this;
        }

        public ServiceRegistry RegisterSingleton<TService>(Func<IServiceResolver, TService> factory)
        {
            _factories[typeof(TService)] = r =>
            {
                if (!_singletons.TryGetValue(typeof(TService), out var cached))
                {
                    cached = factory(r)!;
                    _singletons[typeof(TService)] = cached;
                }
                return cached;
            };

            return this;
        }

        public ServiceRegistry RegisterSingleton<TService, TImpl>() where TImpl : TService, new()
            => RegisterSingleton<TService>(_ => new TImpl());

        public ServiceRegistry RegisterFactory<TService>(Func<IServiceResolver, TService> factory)
        {
            _factories[typeof(TService)] = r => factory(r)!;
            return this;
        }

        public ServiceRegistry RegisterFactory<TService, TImpl>() where TImpl : TService, new()
            => RegisterFactory<TService>(_ => new TImpl());

        public IServiceResolver Build() => new ServiceContainer(_factories, _singletons);
    }
}
