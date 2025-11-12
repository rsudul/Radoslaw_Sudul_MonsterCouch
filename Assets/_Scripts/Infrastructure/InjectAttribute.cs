using System;

namespace MonsterCouchTest.Infrastructure
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InjectAttribute : Attribute { }
}
