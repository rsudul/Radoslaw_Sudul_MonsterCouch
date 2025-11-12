using UnityEngine;
using MonsterCouchTest.Gameplay.World;

namespace MonsterCouchTest.Infrastructure
{
    public static class ServiceRegistration
    {
        public static ServiceRegistry AddGameSystems(this ServiceRegistry reg, Camera camera, float boundsMargin)
        {
            reg.RegisterSingleton<IBoundsProvider>(_ => new CameraBoundsProvider(camera, boundsMargin));
            return reg;
        }
    }
}
