using System;

namespace MonsterCouchTest.Infrastructure
{
    public interface IServiceResolver
    {
        T Resolve<T>();
        object Resolve(Type type);

        bool TryResolve<T>(out T service);
        bool TryResolve(Type type, out object service);
    }
}
