using UnityEngine;
using MonsterCouchTest.Gameplay.AI;

namespace MonsterCouchTest.Input
{
    public sealed class FleeInputSource : IInputSource
    {
        private readonly Transform _self;
        private readonly ITargetProvider _threat;

        public FleeInputSource(Transform self, ITargetProvider threat)
        {
            _self = self;
            _threat = threat;
        }

        public Vector2 ReadMove()
        {
            if (_self == null || _threat == null)
            {
                return Vector2.zero;
            }

            var away = (Vector2)(_self.position - _threat.Position);
            return away.sqrMagnitude > 1e-6f ? away.normalized : Vector2.zero;
        }
    }
}
