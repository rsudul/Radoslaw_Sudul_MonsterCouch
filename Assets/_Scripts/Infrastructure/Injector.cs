using System.Linq;
using System.Reflection;

namespace MonsterCouchTest.Infrastructure
{
    public static class Injector
    {
        public static void InjectInto(object target, IServiceResolver resolver)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var t = target.GetType();

            foreach (var f in t.GetFields(flags).Where(f => f.IsDefined(typeof(InjectAttribute), true)))
            {
                var service = resolver.Resolve(f.FieldType);
                f.SetValue(target, service);
            }

            foreach (var p in t.GetProperties(flags).Where(p =>
                p.IsDefined(typeof(InjectAttribute), true) && p.CanWrite))
            {
                var service = resolver.Resolve(p.PropertyType);
                p.SetValue(target, service);
            }
        }
    }
}
