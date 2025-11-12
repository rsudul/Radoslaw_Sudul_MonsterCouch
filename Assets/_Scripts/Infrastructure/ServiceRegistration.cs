using UnityEngine;

namespace MonsterCouchTest.Infrastructure
{
    public static class ServiceRegistration
    {
        public static ServiceRegistry AddGameSystems(this ServiceRegistry reg)
        {
            return reg;
        }
    }
}
